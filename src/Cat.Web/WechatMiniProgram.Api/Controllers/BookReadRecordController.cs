using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatBookApp.BookReadRecords;
using CatBookApp.BookReadRecords.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WechatMiniProgram.Api.Controllers
{
    /// <summary>
    /// 书本阅读记录表
    /// </summary>
    public class BookReadRecordController : CatControllerBase
    {
        private readonly IBookReadRecordAppService _bookReadRecordAppService;

        public BookReadRecordController(IBookReadRecordAppService bookReadRecordAppService)
        {
            _bookReadRecordAppService = bookReadRecordAppService;
        }


        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionRes> CreateOrUpdateAsync([FromBody]CreateBookReadRecordDto input)
        {
            var res = await _bookReadRecordAppService.CreateOrUpdateAsync(input);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 分页获取用户最近的阅读记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionRes> GetPagedByLastReadingAsync(GetPagedByLastReadingInput input)
        {
            var res = await _bookReadRecordAppService.GetPagedByLastReadingAsync(input);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 更新阅读时长
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task UpdateDurationAsync(long id, int seconds)
        {
            if (id > 0 && seconds > 0) await _bookReadRecordAppService.UpdateDurationAsync(id, seconds);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionRes> GetAsync(long id)
        {
            var res = await _bookReadRecordAppService.GetAsync(id);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 书本阅读记录汇总信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns>已阅读书本数、章节数、时长(min)</returns>
        [HttpGet]
        public async Task<ActionRes> GetBookReadRecordSummaryAsync(string openid)
        {
            var res = await _bookReadRecordAppService.GetBookReadRecordSummaryAsync(openid);

            return ActionRes.Success(res);
        }
    }
}