using Abp.Application.Services;
using Abp.Domain.Entities;
using Abp.UI;
using CatBookApp.BookApiWhiteLists;
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
        private readonly IBookApiWhiteListAppService _bookApiWhiteListAppService;

        public WechatAppService(IBookApiWhiteListAppService bookApiWhiteListAppService)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"Configs{Path.DirectorySeparatorChar}wechatsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
            _bookApiWhiteListAppService = bookApiWhiteListAppService;
        }

        /// <summary>
        /// auth.code2Session 登录凭证校验
        /// </summary>
        /// <param name="js_code">登录时获取的 code</param>
        /// <param name="appid"></param>
        /// <returns></returns>
        public GetJscode2sessionOutput Jscode2session(string js_code, string appid = "")
        {
            if (string.IsNullOrEmpty(js_code)) throw new EntityNotFoundException("js_code不能为空");

            var _appid = _configuration["Book:Appid"].ToString();
            var _secret = _configuration["Book:Secret"].ToString();

            if (string.IsNullOrEmpty(_appid) || string.IsNullOrEmpty(_secret)) throw new EntityNotFoundException("[wechatsettings.json]没有正确配置appid/secret");

            if (_appid != appid)
            {
                //传入的appid跟配置文件的不一致：表示客户端appid是第三方访问时带入的
                //查看白名单中有无配置此appid
                var entity = _bookApiWhiteListAppService.GetByAppidAsync(appid).Result;
                if (entity == null || entity.ExpireTime < DateTime.Now) throw new UserFriendlyException("抱歉，小说接口不能用于小程序线上环境（体验版、正式版），仅供开发版学习测试所用！请自行配置为本地后端api接口，则不受此限制。");
                if (string.IsNullOrEmpty(entity.AppSecret)) throw new UserFriendlyException("小程序AppSecret未配置");
                _appid = appid;
                _secret = entity.AppSecret;
            }

            var url = string.Format("https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", _appid, _secret, js_code);

            var response = Utils.HttpHelper.Get(url);

            return JsonConvert.DeserializeObject<GetJscode2sessionOutput>(response);
        }
    }
}
