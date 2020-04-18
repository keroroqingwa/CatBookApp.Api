using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatBookApp.BookUserPreferences;
using CatBookApp.BookUserPreferences.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WechatMiniProgram.Api.Controllers
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
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionRes> CreateOrUpdateAsync([FromBody]CreateOrUpdateDto input)
        {
            var res = await _bookUserPreferenceAppService.CreateOrUpdateAsync(input);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionRes> GetByOpenidAsync(string openid)
        {
            var res = await _bookUserPreferenceAppService.GetByOpenidAsync(openid);

            return ActionRes.Success(res);
        }
    }
}