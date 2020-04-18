using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatBookApp.BookUsers;
using CatBookApp.BookUsers.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WechatMiniProgram.Api.Controllers
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
        /// 新增或更新
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionRes> CreateOrUpdateAsync([FromBody]CreateBookUserDto dto)
        {
            var bookUserOutput = await _bookUserAppService.GetByOpenidAsync(dto.Openid);

            if (bookUserOutput == null)
            {
                await _bookUserAppService.CreateAsync(dto);
            }
            else
            {
                await _bookUserAppService.UpdateAsync(ObjectMapper.Map<UpdateBookUserDto>(bookUserOutput));
            }

            return ActionRes.Success();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionRes> GetByOpenid(string openid)
        {
            var res = await _bookUserAppService.GetByOpenidAsync(openid);

            return ActionRes.Success(res);
        }
    }
}