using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CatBookApp.BookReadRecords;
using CatBookApp.BookReadRecords.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Backstage.Api.Controllers
{
    /// <summary>
    /// 书本阅读记录
    /// </summary>
    public class BookReadRecordController : CatControllerBase
    {
        private readonly IBookReadRecordAppService _bookReadRecordAppService;

        public BookReadRecordController(IBookReadRecordAppService bookReadRecordAppService)
        {
            _bookReadRecordAppService = bookReadRecordAppService;
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
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionRes> GetPagedAsync(GetPagedInput input)
        {
            var res = await _bookReadRecordAppService.GetPagedAsync(input);

            return ActionRes.Success(res);
        }
    }
}