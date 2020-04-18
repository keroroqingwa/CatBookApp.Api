using Abp.Application.Services;
using Abp.Domain.Entities;
using CatBookApp.Wechat.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CatBookApp.Wechat
{
    /// <summary>
    /// 微信相关 应用服务层实现
    /// </summary>
    public class WechatAppService : ApplicationService, IWechatAppService
    {
        private readonly IConfiguration _configuration;

        public WechatAppService()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"Configs{Path.DirectorySeparatorChar}wechatsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        /// <summary>
        /// auth.code2Session 登录凭证校验
        /// </summary>
        /// <param name="js_code">登录时获取的 code</param>
        /// <returns></returns>
        public GetJscode2sessionOutput Jscode2session(string js_code)
        {
            if (string.IsNullOrEmpty(js_code)) throw new EntityNotFoundException("js_code不能为空");

            var appid = _configuration["Book:Appid"].ToString();
            var secret = _configuration["Book:Secret"].ToString();

            if (string.IsNullOrEmpty(appid) || string.IsNullOrEmpty(secret)) throw new EntityNotFoundException("[wechatsettings.json]没有正确配置appid/secret");

            var url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", appid, secret, js_code);

            var response = Utils.HttpHelper.Get(url);

            return JsonConvert.DeserializeObject<GetJscode2sessionOutput>(response);
        }
    }
}
