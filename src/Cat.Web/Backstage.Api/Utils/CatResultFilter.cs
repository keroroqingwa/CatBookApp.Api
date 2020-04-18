using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.Mvc.Results;
using Abp.AspNetCore.Mvc.Results.Wrapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Api.Utils
{
    public class CatResultFilter : AbpResultFilter
    {
        private readonly IAbpAspNetCoreConfiguration _configuration;
        private readonly IAbpActionResultWrapperFactory _actionResultWrapper;

        public CatResultFilter(IAbpAspNetCoreConfiguration configuration, IAbpActionResultWrapperFactory actionResultWrapper) : base(configuration, actionResultWrapper)
        {
            _configuration = configuration;
            _actionResultWrapper = actionResultWrapper;
        }


        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ContentResult contentResult = new ContentResult();

            if (filterContext.Result is ObjectResult)
            {
                var objectResult = filterContext.Result as ObjectResult;

                if (objectResult == null) return;
                if (objectResult.Value.GetType().Name == "ActionRes")
                {
                    filterContext.Result = new JsonResult(objectResult.Value);
                }
                else
                {
                    //封装其他返回类型
                    filterContext.Result = new JsonResult(ActionRes.Success(objectResult.Value));
                }
            }
            else if (filterContext.Result is EmptyResult)
            {
                filterContext.Result = new JsonResult(new { Code = 404, Msg = "未找到资源", Data = new object() });
            }
            else if (filterContext.Result is ContentResult)
            {
                filterContext.Result = new JsonResult(new { Code = 200, Msg = "", Data = (filterContext.Result as ContentResult).Content });
            }
            else if (filterContext.Result is StatusCodeResult)
            {
                filterContext.Result = new JsonResult(new { Code = (filterContext.Result as StatusCodeResult).StatusCode, Msg = "", Data = new object() });
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }
    }
}
