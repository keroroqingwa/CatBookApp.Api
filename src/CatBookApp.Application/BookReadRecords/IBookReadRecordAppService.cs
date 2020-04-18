using Abp.Application.Services.Dto;
using CatBookApp.BookReadRecords.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatBookApp.BookReadRecords
{
    /// <summary>
    /// 书本阅读记录表 应用服务层接口
    /// </summary>
    public interface IBookReadRecordAppService
    {
        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookReadRecordDto> CreateOrUpdateAsync(CreateBookReadRecordDto input);

        /// <summary>
        /// 分页获取用户最近的阅读记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<LastReadingOutput>> GetPagedByLastReadingAsync(GetPagedByLastReadingInput input);

        /// <summary>
        /// 更新阅读时长
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        Task UpdateDurationAsync(long id, int seconds);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BookReadRecordOutput> GetAsync(long id);

        /// <summary>
        /// 书本阅读记录汇总信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns>已阅读书本数、章节数、时长(min)</returns>
        Task<BookReadRecoredStatisticOutput> GetBookReadRecordSummaryAsync(string openid);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BookReadRecordOutput>> GetPagedAsync(GetPagedInput input);
    }
}
