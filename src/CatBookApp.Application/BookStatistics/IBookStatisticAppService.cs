using CatBookApp.BookStatistics.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookStatistics
{
    /// <summary>
    /// book统计相关 应用服务层接口
    /// </summary>
    public interface IBookStatisticAppService
    {
        /// <summary>
        /// book用户增长统计
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        XYAxisStatisticOutput GetBookUserRiseStatistic(string dateType, DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 时间段（每小时）新增用户统计
        /// </summary>
        /// <returns></returns>
        List<TimeSlotStatisticOutput> GetBookUserTimeSlotStatistic();

        /// <summary>
        /// 阅读了指定章节数的用户数量统计
        /// </summary>
        /// <returns></returns>
        ReadChaptersStatisticOutput GetBookUserReadChaptersStatistic();

        /// <summary>
        /// 时间段（每小时）阅读小说数、时间段（每小时）阅读小说章节数 统计
        /// </summary>
        BookReadTimeSlotStatisticsOutput GetBookReadTimeSlotStatistics();

        /// <summary>
        /// 最受欢迎小说排行 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        List<FlagStatisticsOutput> GetMostPopularBook(int top = 50);

        /// <summary>
        /// 阅读排行榜 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        List<ReadRankingStatisticsOutput> GetReadRanking(int top = 50);
    }
}
