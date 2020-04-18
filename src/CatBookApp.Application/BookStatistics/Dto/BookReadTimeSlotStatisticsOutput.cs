using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookStatistics.Dto
{
    public class BookReadTimeSlotStatisticsOutput
    {
        public List<int> columns { get; set; }
        public List<int> data_book { get; set; }
        public List<int> data_chapter { get; set; }
        public List<int> data_user { get; set; }
    }
}
