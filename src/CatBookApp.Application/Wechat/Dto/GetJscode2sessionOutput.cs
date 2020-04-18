using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.Wechat.Dto
{
    /// <summary>
    /// 登录凭证校验 接口响应实体
    /// </summary>
    public class GetJscode2sessionOutput
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public string Openid { get; set; }

        /// <summary>
        /// 会话密钥
        /// </summary>
        public string Session_key { get; set; }

        /// <summary>
        /// 用户在开放平台的唯一标识符，在满足 UnionID 下发条件的情况下会返回，详见 UnionID 机制说明。
        /// </summary>
        public string Unionid { get; set; }

        /// <summary>
        /// 错误码, -1:系统繁忙，此时请开发者稍候再试, 0:请求成功, 40029:code 无效, 45011:频率限制，每个用户每分钟100次
        /// </summary>
        public int Errcode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Errmsg { get; set; }
    }
}
