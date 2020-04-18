using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookStatistics.Dto
{
    public class ReadRankingStatisticsOutput
    {
        public string Openid { get; set; }
        public string NickName { get; set; }
        public int total { get; set; }
    }
}
