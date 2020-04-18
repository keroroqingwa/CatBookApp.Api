using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using CatBookApp.BookUsers;
using CatBookApp.BookUsers.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Backstage.Api.Controllers
{
    /// <summary>
    /// book用户
    /// </summary>
    public class BookUserController : CatControllerBase
    {
        private readonly IBookUserAppService _bookUserAppService;

        public BookUserController(IBookUserAppService bookUserAppService)
        {
            _bookUserAppService = bookUserAppService;
        }


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionRes> GetPagedAsync(GetPagedInput input)
        {
            var res = await _bookUserAppService.GetPagedAsync(input);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionRes> GetByOpenidAsync([Required]string openid)
        {
            var res = await _bookUserAppService.GetByOpenidAsync(openid);

            return ActionRes.Success(res);
        }
    }
}