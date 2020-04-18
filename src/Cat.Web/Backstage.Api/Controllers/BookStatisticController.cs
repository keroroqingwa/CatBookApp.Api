using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CatBookApp.BookStatistics;
using Microsoft.AspNetCore.Mvc;

namespace Backstage.Api.Controllers
{
    /// <summary>
    /// book统计相关
    /// </summary>
    public class BookStatisticController : CatControllerBase
    {
        private readonly IBookStatisticAppService _bookStatisticAppService;

        public BookStatisticController(IBookStatisticAppService bookStatisticAppService)
        {
            _bookStatisticAppService = bookStatisticAppService;
        }


        /// <summary>
        /// book用户增长统计
        /// </summary>
        /// <param name="dateType">统计类型，y、m、d</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetBookUserRiseStatistic([Required]string dateType, DateTime beginTime, DateTime endTime)
        {
            var res = _bookStatisticAppService.GetBookUserRiseStatistic(dateType, beginTime, endTime);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 时间段（每小时）新增用户统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetBookUserTimeSlotStatistic()
        {
            var res = _bookStatisticAppService.GetBookUserTimeSlotStatistic();

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 阅读了指定章节数的用户数量统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetBookUserReadChaptersStatistic()
        {
            var res = _bookStatisticAppService.GetBookUserReadChaptersStatistic();

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 时间段（每小时）阅读小说数、时间段（每小时）阅读小说章节数 统计
        /// </summary>
        [HttpGet]
        public ActionRes GetBookReadTimeSlotStatistics()
        {
            var res = _bookStatisticAppService.GetBookReadTimeSlotStatistics();

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 最受欢迎小说排行 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetMostPopularBook(int top = 50)
        {
            var res = _bookStatisticAppService.GetMostPopularBook();

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 阅读排行榜 top
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetReadRanking(int top = 50)
        {
            var res = _bookStatisticAppService.GetReadRanking();

            return ActionRes.Success(res);
        }
    }
}