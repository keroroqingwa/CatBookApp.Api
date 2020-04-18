using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookStatistics.Dto
{
    public class ReadChaptersStatisticOutput
    {
        public int NoRead { get; set; }
        public int LessThan10 { get; set; }
        public int Between10and100 { get; set; }
        public int Between101and500 { get; set; }
        public int MoreThan500 { get; set; }
    }
}
