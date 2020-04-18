using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookSearches.Dto
{
    public class BookInfoDto
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
        public string BookClassify { get; set; }
        public string BookLink { get; set; }
        public string BookIntro { get; set; }
        public string Last_Update_Time { get; set; }
        public string Last_Update_ChapterName { get; set; }
        public string Last_Update_ChapterLink { get; set; }
    }
}
