using Abp.Domain.Services;
using CatBookApp.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.DomainServices.BookStatistics
{
    /// <summary>
    /// book相关统计 领域服务
    /// </summary>
    public class BookStatisticManager : DomainService
    {
        private readonly ISqlExecuterRepository _sqlExecuterRepository;

        public BookStatisticManager(ISqlExecuterRepository sqlExecuterRepository)
        {
            _sqlExecuterRepository = sqlExecuterRepository;
        }


        /// <summary>
        /// book用户增长统计
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<T> GetBookUserRiseStatistic<T>(string dateType, DateTime beginTime, DateTime endTime)
        {
            StringBuilder sbSql = new StringBuilder();

            switch (dateType)
            {
                case "y":
                    //--按年统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(CreationTime, '%Y') as 'y',");
                    sbSql.Append(" count(1) as total");
                    sbSql.Append(" from book_user");
                    sbSql.Append(" where CreationTime between @beginTime and @endTime");
                    sbSql.Append(" group by date_format(CreationTime, '%Y')");
                    sbSql.Append(" order by CreationTime asc;");
                    break;
                case "m":
                    //--按月统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(CreationTime, '%Y') as 'y',");
                    sbSql.Append(" date_format(CreationTime, '%m') as 'm',");
                    sbSql.Append(" count(1) as total");
                    sbSql.Append(" from book_user");
                    sbSql.Append(" where CreationTime between @beginTime and @endTime");
                    sbSql.Append(" group by date_format(CreationTime, '%Y%m')");
                    sbSql.Append(" order by CreationTime asc;");
                    break;
                case "d":
                    //--按日统计
                    sbSql.Append(" select");
                    sbSql.Append(" date_format(CreationTime, '%Y') as 'y',");
                    sbSql.Append(" date_format(CreationTime, '%m') as 'm',");
                    sbSql.Append(" date_format(CreationTime, '%d') as 'd',");
                    sbSql.Append(" count(1) as total");
                    sbSql.Append(" from book_user");
                    sbSql.Append(" where CreationTime between @beginTime and @endTime");
                    sbSql.Append(" group by date_format(CreationTime, '%Y%m%d')");
                    sbSql.Append(" order by CreationTime asc;");
                    break;
            }

            sbSql.Replace("@beginTime", $"'{beginTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");
            sbSql.Replace("@endTime", $"'{endTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");

            var list = _sqlExecuterRepository.ExecuteQuery<T>(sbSql.ToString());

            return list;
        }

        /// <summary>
        /// 时间段（每小时）新增用户统计
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<T> GetBookUserTimeSlotStatistic<T>(DateTime beginTime, DateTime endTime)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(" select * from (");
            sbSql.Append(" select");
            sbSql.Append(" date_format(CreationTime,'%H') as 'h',");
            sbSql.Append(" count(Openid) as total");
            sbSql.Append(" from book_user");
            sbSql.Append(" where CreationTime between @beginTime and @endTime");
            sbSql.Append(" group by date_format(CreationTime,'%H')");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.h asc;");

            sbSql.Replace("@beginTime", $"'{beginTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");
            sbSql.Replace("@endTime", $"'{endTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");

            var list = _sqlExecuterRepository.ExecuteQuery<T>(sbSql.ToString());

            return list;
        }

        /// <summary>
        /// 从未阅读过的用户数量
        /// </summary>
        /// <param name="minVal"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        public List<T> GetBookUserNoReadChaptersStatistic<T>()
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("select count(Openid) as total from book_user where Openid not in (select Openid from book_readrecordreport);");

            var list = _sqlExecuterRepository.ExecuteQuery<T>(sbSql.ToString());

            return list;
        }

        /// <summary>
        /// 阅读了指定章节数的用户数量
        /// </summary>
        /// <param name="minVal"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        public List<T> GetBookUserReadChaptersStatistic<T>(int minVal, int maxVal)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("select count(1) as total from (select count(Openid), Openid from book_readrecord GROUP BY Openid having count(Openid) between @minVal and @maxVal) t;");

            sbSql.Replace("@minVal", minVal.ToString());
            sbSql.Replace("@maxVal", maxVal.ToString());

            var list = _sqlExecuterRepository.ExecuteQuery<T>(sbSql.ToString());

            return list;
        }

        /// <summary>
        /// 时间段（每小时）阅读小说数 统计
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<T> BookReadTimeSlotStatistics<T>(DateTime beginTime, DateTime endTime)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(" select * from (");
            sbSql.Append(" select");
            sbSql.Append(" date_format(CreationTime,'%H') as 'h',");
            sbSql.Append(" count(distinct(Id)) as total");
            sbSql.Append(" from book_readrecordreport");
            sbSql.Append(" where CreationTime between @beginTime and @endTime");
            sbSql.Append(" group by date_format(CreationTime,'%H')");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.h asc;");

            sbSql.Replace("@beginTime", $"'{beginTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");
            sbSql.Replace("@endTime", $"'{endTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");

            var list = _sqlExecuterRepository.ExecuteQuery<T>(sbSql.ToString());

            return list;
        }

        /// <summary>
        /// 时间段（每小时）阅读小说章节数 统计
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<T> BookReadChapterTimeSlotStatistics<T>(DateTime beginTime, DateTime endTime)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(" select * from (");
            sbSql.Append(" select");
            sbSql.Append(" date_format(CreationTime,'%H') as 'h',");
            sbSql.Append(" count(1) as total");
            sbSql.Append(" from book_readrecord");
            sbSql.Append(" where CreationTime between @beginTime and @endTime");
            sbSql.Append(" group by date_format(CreationTime,'%H')");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.h asc;");

            sbSql.Replace("@beginTime", $"'{beginTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");
            sbSql.Replace("@endTime", $"'{endTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");

            var list = _sqlExecuterRepository.ExecuteQuery<T>(sbSql.ToString());

            return list;
        }

        /// <summary>
        /// 时间段（每小时）参与阅读的用户数 统计
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<T> BookUserReadTimeSlotStatistics<T>(DateTime beginTime, DateTime endTime)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(" select * from (");
            sbSql.Append(" select");
            sbSql.Append(" date_format(CreationTime,'%H') as 'h',");
            sbSql.Append(" count(distinct(Openid)) as total");
            sbSql.Append(" from book_readrecord");
            sbSql.Append(" where CreationTime between @beginTime and @endTime");
            sbSql.Append(" group by date_format(CreationTime,'%H')");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.h asc;");

            sbSql.Replace("@beginTime", $"'{beginTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");
            sbSql.Replace("@endTime", $"'{endTime.ToString("yyyy/MM/dd HH:mm:ss.fff")}'");

            var list = _sqlExecuterRepository.ExecuteQuery<T>(sbSql.ToString());

            return list;
        }

        /// <summary>
        /// 最受欢迎小说排行 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<T> GetMostPopularBook<T>(int top = 50)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(" select * from (");
            sbSql.Append(" select BookName as flag, count(distinct(Openid)) as total");
            sbSql.Append(" from book_readrecordreport");
            sbSql.Append(" group by BookName");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.total desc");
            sbSql.Append(" limit @top");

            sbSql.Replace("@top", top.ToString());

            var list = _sqlExecuterRepository.ExecuteQuery<T>(sbSql.ToString());

            return list;
        }

        /// <summary>
        /// 阅读排行榜 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<T> GetReadRanking<T>(int top = 50)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append(" select *,");
            sbSql.Append(" (select NickName from book_user where book_user.Openid = t.Openid limit 1) as NickName");
            sbSql.Append(" from (");
            sbSql.Append(" select Openid, count(Openid) as total");
            sbSql.Append(" from book_readrecord");
            sbSql.Append(" group by Openid");
            sbSql.Append(" ) t");
            sbSql.Append(" order by t.total desc");
            sbSql.Append(" limit @top");

            sbSql.Replace("@top", top.ToString());

            var list = _sqlExecuterRepository.ExecuteQuery<T>(sbSql.ToString());

            return list;
        }
    }
}
