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

        private static object lockobj = new object();

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
            //临时方案，只适合单机部署的站点使用。如果是分布式，请用redis分布式并发锁等技术
            lock (lockobj)
            {
                var bookUserOutput = _bookUserAppService.GetByOpenidAsync(dto.Openid).Result;

                if (bookUserOutput == null)
                {
                    _bookUserAppService.CreateAsync(dto).Wait();
                }
                else
                {
                    _bookUserAppService.UpdateAsync(ObjectMapper.Map<UpdateBookUserDto>(bookUserOutput)).Wait();
                }
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