using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.Mvc.Extensions;
using Abp.AspNetCore.Mvc.Results;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
using Abp.Runtime.Validation;
using Abp.UI;
using Abp.Web.Models;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Backstage.Api.Utils
{
    /// <summary>
    /// 自定义异常过滤
    /// </summary>
    public class CatExceptionFilter : IExceptionFilter, ITransientDependency
    {
        // 日志记录器
        public ILogger Logger { get; set; }

        // 事件总线
        public IEventBus EventBus { get; set; }

        // 错误信息构建器
        private readonly IErrorInfoBuilder _errorInfoBuilder;
        // AspNetCore 相关的配置信息
        private readonly IAbpAspNetCoreConfiguration _configuration;

        // 注入并初始化内部成员对象
        public CatExceptionFilter(IErrorInfoBuilder errorInfoBuilder, IAbpAspNetCoreConfiguration configuration)
        {
            _errorInfoBuilder = errorInfoBuilder;
            _configuration = configuration;

            Logger = NullLogger.Instance;
            EventBus = NullEventBus.Instance;
        }

        // 异常触发时会调用此方法
        public void OnException(ExceptionContext context)
        {
            // 判断是否由控制器触发，如果不是则不做任何处理
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            HandleAndWrapException(context);
        }

        // 处理并包装异常
        private void HandleAndWrapException(ExceptionContext context)
        {
            // 判断被调用接口的返回值是否符合标准，不符合则直接返回
            if (!ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
            {
                return;
            }

            // 设置 HTTP 上下文响应所返回的错误代码，由具体异常决定。
            context.HttpContext.Response.StatusCode = GetStatusCode(context);

            //// 重新封装响应返回的具体内容。采用 AjaxResponse 进行封装
            //context.Result = new ObjectResult(
            //    new AjaxResponse(
            //        _errorInfoBuilder.BuildForException(context.Exception),
            //        context.Exception is AbpAuthorizationException
            //    )
            //);

            //自定义响应数据格式
            var errInfo = _errorInfoBuilder.BuildForException(context.Exception);
            var sb = new StringBuilder();
            if (errInfo.ValidationErrors != null)
            {
                foreach (var err in errInfo.ValidationErrors)
                {
                    sb.AppendLine($"【{string.Join(',', err.Members)}】：{err.Message}");
                }
            }
            else
            {
                sb.Append(context.Exception.Message);
            }
            errInfo.Code = -1;

            ContentResult content = new ContentResult();
            content.Content = JsonConvert.SerializeObject(ActionRes.Fail(-1, sb.ToString(), errInfo), new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
            context.Result = content;

            // 触发异常处理事件
            EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));

            // 处理完成，将异常上下文的内容置为空
            context.Exception = null; //Handled!
        }

        // 根据不同的异常类型返回不同的 HTTP 错误码
        protected virtual int GetStatusCode(ExceptionContext context)
        {
            if (context.Exception is AbpAuthorizationException)
            {
                return context.HttpContext.User.Identity.IsAuthenticated
                    ? (int)HttpStatusCode.Forbidden
                    : (int)HttpStatusCode.Unauthorized;
            }

            if (context.Exception is AbpValidationException)
            {
                return (int)HttpStatusCode.BadRequest;
            }

            if (context.Exception is EntityNotFoundException)
            {
                return (int)HttpStatusCode.InternalServerError;
            }

            if (context.Exception is UserFriendlyException)
            {
                return (int)HttpStatusCode.OK;
            }

            return (int)HttpStatusCode.InternalServerError;
        }
    }
}
