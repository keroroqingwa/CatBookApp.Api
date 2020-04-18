using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookSearches.Dto
{
    public class BookContentDto
    {
        public string BookName { get; set; }
        public string BookLink { get; set; }
        public string ChapterName { get; set; }
        public string ChapterLink { get; set; }
        public string Content { get; set; }
        public string NextChapterLink { get; set; }
        public string PrevChapterLink { get; set; }
        public int Number_Of_Words { get; set; }
    }
}
