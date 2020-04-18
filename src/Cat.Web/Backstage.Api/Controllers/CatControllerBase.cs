using Abp.AspNetCore.Mvc.Controllers;
using Abp.Authorization;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    //[ApiController] //不能加此特性，restful规范问题，导致get请求的方法不能以实体方式传入参数
    [IgnoreAntiforgeryToken] //禁用Antiforgery验证，即不做CSRF防御
    [DontWrapResult]
    [AbpAuthorize]
    public abstract class CatControllerBase : AbpController
    {
    }
}
