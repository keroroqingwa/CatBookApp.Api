using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookSearches.Dto
{
    public class BookChapterDto
    {
        public string BookName { get; set; }
        public string BookLink { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public string Last_Update_Time { get; set; }
        public string Last_Update_ChapterName { get; set; }
        public string Last_Update_ChapterLink { get; set; }
        public string Intro { get; set; }
        public List<ChapterlistModel> Chapterlist { get; set; }
        public class ChapterlistModel
        {
            public string ChapterName { get; set; }
            public string ChapterLink { get; set; }
        }
    }
}
