using Abp.Dependency;
using Abp.UI;
using CatBookApp.BookApiWhiteLists;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WechatMiniProgram.Api.Utils
{
    /// <summary>
    /// book api 接口请求拦截. 验证请求的[appid]是否在后台设置的白名单中
    /// </summary>
    public class BookApiAccessFilter : Attribute, IActionFilter
    {
        private readonly IIocResolver _iocResolver;

        public BookApiAccessFilter()
        {
            _iocResolver = IocManager.Instance;
        }


        /// <summary>
        /// Action执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Action执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var referer = context.HttpContext.Request.Headers["Referer"].ToString();

            //if (string.IsNullOrEmpty(referer)) return;
            //允许通过swagger页面请求接口
            if (!referer.Contains("servicewechat.com") && referer.EndsWith("/index.html")) return;

            Regex MyRegex = new Regex(
                              "servicewechat\\.com\\/(?<appid>.*?)/(?<version>.*?)/page-frame\\.html",
                            RegexOptions.IgnoreCase
                            | RegexOptions.Multiline
                            | RegexOptions.IgnorePatternWhitespace
                            | RegexOptions.Compiled
                            );

            Match m = MyRegex.Match(referer);

            if (m == null || m.Groups.Count == 0) throw new UserFriendlyException("Referer没有匹配到appid值，请在小程序端访问接口");

            string appid = m.Groups["appid"].Value;
            string version = m.Groups["version"].Value; //version: devtools-开发版，0-体验版，大于0表示线上正式版本

            if (string.IsNullOrEmpty(appid) || string.IsNullOrEmpty(version)) throw new UserFriendlyException("Referer没有匹配到appid值，请在小程序端访问接口");

            //如果是“体验版”或“正式版本”
            if (IsNumber(version) && !IsSelfAppid(appid))
            {
                //测试访问来源站点是否在白名单内
                using (var _bookApiWhiteListAppService = _iocResolver.ResolveAsDisposable<IBookApiWhiteListAppService>())
                {
                    var entity = _bookApiWhiteListAppService.Object.GetByAppidAsync(appid).Result;
                    if (entity == null || entity.ExpireTime < DateTime.Now) throw new UserFriendlyException("抱歉，小说接口不能用于小程序线上环境（体验版、正式版），仅供开发版学习测试所用！请自行配置为本地后端api接口，则不受此限制。");
                }
            }
        }

        /// <summary>
        /// 检查请求中的appid是否为程序中配置的appid一致
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        private bool IsSelfAppid(string appid)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"Configs{Path.DirectorySeparatorChar}wechatsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var _configuration = builder.Build();
            return _configuration["Book:Appid"] == appid;

        }

        /// <summary>
        /// 检测字符串是否为数字
        /// </summary>
        /// <param name="input">需要检查的字符串</param>
        /// <returns>如果字符串为数字，则为 true；否则为 false。</returns>
        private bool IsNumber(string input)
        {
            return !string.IsNullOrEmpty(input) && new Regex(@"^[0-9]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled).IsMatch(input);
        }
    }
}
