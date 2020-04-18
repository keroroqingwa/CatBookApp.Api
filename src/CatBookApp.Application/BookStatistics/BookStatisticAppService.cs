using Abp.Application.Services;
using CatBookApp.BookStatistics.Dto;
using CatBookApp.DomainServices.BookStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatBookApp.BookStatistics
{
    /// <summary>
    /// book统计相关 应用服务层实现
    /// </summary>
    public class BookStatisticAppService : ApplicationService, IBookStatisticAppService
    {
        private readonly BookStatisticManager _bookStatisticManager;

        public BookStatisticAppService(BookStatisticManager bookStatisticManager)
        {
            _bookStatisticManager = bookStatisticManager;
        }


        /// <summary>
        /// 根据列表数据，获取x坐标轴的集合
        /// </summary>
        /// <param name="dateType"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<string> GetColumns<T>(string dateType, IList<T> list) where T : DateTotalStatisticsDto
        {
            var columns = new List<string>();
            if (dateType == "y")
            {
                foreach (var item in list)
                {
                    columns.Add(string.Format("{0}年", item.y));
                }
            }
            else if (dateType == "m")
            {
                foreach (var item in list)
                {
                    columns.Add(string.Format("{0}.{1}", item.y, item.m.ToString().PadLeft(2, '0')));
                }
            }
            else if (dateType == "d")
            {
                foreach (var item in list)
                {
                    columns.Add(string.Format("{1}.{2}", item.y, item.m.ToString().PadLeft(2, '0'), item.d.ToString().PadLeft(2, '0')));
                }
            }

            return columns;
        }

        /// <summary>
        /// book用户增长统计
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public XYAxisStatisticOutput GetBookUserRiseStatistic(string dateType, DateTime beginTime, DateTime endTime)
        {
            var list = _bookStatisticManager.GetBookUserRiseStatistic<DateTotalStatisticsDto>(dateType, beginTime, endTime);

            List<string> columns = GetColumns(dateType, list);

            var data = new XYAxisStatisticOutput
            {
                xAxis = columns,
                yAxis = list.Select(s => s.total).ToList(),
            };

            return data;
        }

        /// <summary>
        /// 时间段（每小时）新增用户统计
        /// </summary>
        /// <returns></returns>
        public List<TimeSlotStatisticOutput> GetBookUserTimeSlotStatistic()
        {
            DateTime beginTime = DateTime.MinValue.AddMilliseconds(1);
            DateTime endTime = DateTime.MaxValue.AddMilliseconds(-1);

            return _bookStatisticManager.GetBookUserTimeSlotStatistic<TimeSlotStatisticOutput>(beginTime, endTime);
        }

        /// <summary>
        /// 阅读了指定章节数的用户数量统计
        /// </summary>
        /// <returns></returns>
        public ReadChaptersStatisticOutput GetBookUserReadChaptersStatistic()
        {
            //从未阅读过的用户数量
            var s0 = _bookStatisticManager.GetBookUserNoReadChaptersStatistic<TotalStatisticsDto>();

            //阅读低于10章的用户有多少个
            var s1 = _bookStatisticManager.GetBookUserReadChaptersStatistic<TotalStatisticsDto>(0, 9);

            //阅读在10~100章的用户有多少个
            var s2 = _bookStatisticManager.GetBookUserReadChaptersStatistic<TotalStatisticsDto>(10, 100);

            //阅读在101~500章的用户有多少个
            var s3 = _bookStatisticManager.GetBookUserReadChaptersStatistic<TotalStatisticsDto>(101, 500);

            //阅读高于500章的用户有多少个
            var s4 = _bookStatisticManager.GetBookUserReadChaptersStatistic<TotalStatisticsDto>(501, int.MaxValue);

            return new ReadChaptersStatisticOutput()
            {
                NoRead = Convert.ToInt32(s0.FirstOrDefault()?.total),
                LessThan10 = Convert.ToInt32(s1.FirstOrDefault()?.total),
                Between10and100 = Convert.ToInt32(s2.FirstOrDefault()?.total),
                Between101and500 = Convert.ToInt32(s3.FirstOrDefault()?.total),
                MoreThan500 = Convert.ToInt32(s4.FirstOrDefault()?.total),
            };
        }

        /// <summary>
        /// 时间段（每小时）阅读小说数、时间段（每小时）阅读小说章节数 统计
        /// </summary>
        public BookReadTimeSlotStatisticsOutput GetBookReadTimeSlotStatistics()
        {
            DateTime beginTime = DateTime.MinValue.AddMilliseconds(1);
            DateTime endTime = DateTime.MaxValue.AddMilliseconds(-1);

            //时间段（每小时）阅读小说数 统计
            var list = _bookStatisticManager.BookReadTimeSlotStatistics<TimeSlotStatisticOutput>(beginTime, endTime);
            //时间段（每小时）阅读小说章节数 统计
            var list2 = _bookStatisticManager.BookReadChapterTimeSlotStatistics<TimeSlotStatisticOutput>(beginTime, endTime);
            //时间段（每小时）参与阅读的用户数 统计
            var list3 = _bookStatisticManager.BookUserReadTimeSlotStatistics<TimeSlotStatisticOutput>(beginTime, endTime);

            return new BookReadTimeSlotStatisticsOutput
            {
                columns = list.Select(s => s.h).ToList(),
                data_book = list.Select(s => s.total).ToList(),
                data_chapter = list2.Select(s => s.total).ToList(),
                data_user = list3.Select(s => s.total).ToList(),
            };
        }

        /// <summary>
        /// 最受欢迎小说排行 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<FlagStatisticsOutput> GetMostPopularBook(int top = 50)
        {
            var list = _bookStatisticManager.GetMostPopularBook<FlagStatisticsOutput>(top);

            return list;
        }

        /// <summary>
        /// 阅读排行榜 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<ReadRankingStatisticsOutput> GetReadRanking(int top = 50)
        {
            var list = _bookStatisticManager.GetReadRanking<ReadRankingStatisticsOutput>(top);

            return list;
        }
    }
}
