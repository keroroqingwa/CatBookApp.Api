using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.UI;
using CatBookApp.BookApiWhiteLists;
using CatBookApp.BookApiWhiteLists.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Backstage.Api.Controllers
{
    /// <summary>
    /// book相关Api接口白名单
    /// </summary>
    public class BookApiWhiteListController : CatControllerBase
    {
        private readonly IBookApiWhiteListAppService _bookApiWhiteListAppService;

        public BookApiWhiteListController(IBookApiWhiteListAppService bookApiWhiteListAppService)
        {
            _bookApiWhiteListAppService = bookApiWhiteListAppService;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionRes> CreateAsync([FromBody]CreateBookApiWhiteListDto input)
        {
            throw new UserFriendlyException("当前接口暂时关闭，有疑问？加QQ群：875607244");

            var res = await _bookApiWhiteListAppService.CreateAsync(input);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionRes> UpdateAsync([FromBody]BookApiWhiteListDto input)
        {
            throw new UserFriendlyException("当前接口暂时关闭，有疑问？加QQ群：875607244");

            var res = await _bookApiWhiteListAppService.UpdateAsync(input);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        [HttpPost]
        public async Task<ActionRes> DeleteAsync([FromBody]Models.Dto.IdsInput input)
        {
            throw new UserFriendlyException("当前接口暂时关闭，有疑问？加QQ群：875607244");

            await _bookApiWhiteListAppService.DeleteAsync(input.ids);

            return ActionRes.Success();
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionRes> GetPagedAsync(GetPagedInput input)
        {
            var res = await _bookApiWhiteListAppService.GetPagedAsync(input);

            return ActionRes.Success(res);
        }
    }
}