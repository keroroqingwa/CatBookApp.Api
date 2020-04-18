using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookReadRecords.Dto
{
    public class BookReadRecoredStatisticOutput
    {
        public int BookCount { get; set; }
        public int ChapterCount { get; set; }
        public int ReadMinutes { get; set; }

    }
}
