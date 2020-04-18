using Abp.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WechatMiniProgram.Api.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class HomeController : CatControllerBase
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return Content("这是「喵喵看书」微信小程序的后端接口程序，如有任何疑问可加QQ群：875607244");
        }
    }
}
