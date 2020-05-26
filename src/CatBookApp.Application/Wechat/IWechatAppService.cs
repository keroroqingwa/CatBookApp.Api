using CatBookApp.Wechat.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.Wechat
{
    public interface IWechatAppService
    {

        /// <summary>
        /// auth.code2Session 登录凭证校验
        /// </summary>
        /// <param name="js_code">登录时获取的 code</param>
        /// <param name="appid">appid</param>
        /// <returns></returns>
        GetJscode2sessionOutput Jscode2session(string js_code, string appid = "");
    }
}
