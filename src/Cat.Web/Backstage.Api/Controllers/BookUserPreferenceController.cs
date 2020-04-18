using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatBookApp.BookUserPreferences;
using CatBookApp.BookUserPreferences.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Backstage.Api.Controllers
{
    /// <summary>
    /// 用户偏好
    /// </summary>
    public class BookUserPreferenceController : CatControllerBase
    {
        private readonly IBookUserPreferenceAppService _bookUserPreferenceAppService;

        public BookUserPreferenceController(IBookUserPreferenceAppService bookUserPreferenceAppService)
        {
            _bookUserPreferenceAppService = bookUserPreferenceAppService;
        }


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionRes> GetPagedAsync(GetPagedInput input)
        {
            var res = await _bookUserPreferenceAppService.GetPagedAsync(input);

            return ActionRes.Success(res);
        }
    }
}