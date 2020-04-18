using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using CatBookApp.Wechat;
using Microsoft.AspNetCore.Mvc;

namespace WechatMiniProgram.Api.Controllers
{
    /// <summary>
    /// 微信相关
    /// </summary>
    public class WechatController : CatControllerBase
    {
        private readonly IWechatAppService _wechatAppService;

        public WechatController(IWechatAppService wechatAppService)
        {
            _wechatAppService = wechatAppService;
        }

        /// <summary>
        /// 登录凭证校验
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionRes Jscode2session(string code)
        {
            var res = _wechatAppService.Jscode2session(code);

            if (res.Errcode == 0)
                return ActionRes.Success(res);
            else
                return ActionRes.Fail(-1, res.Errmsg, res);
        }
    }
}