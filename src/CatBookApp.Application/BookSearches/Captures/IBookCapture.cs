using CatBookApp.BookSearches.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookSearches.Captures
{
    public interface IBookCapture
    {
        /// <summary>
        /// 搜索书本
        /// </summary>
        /// <param name="q"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        List<BookInfoDto> GetBooks(string q, int pn);

        /// <summary>
        /// 获取小说章节目录信息
        /// </summary>
        /// <param name="bookLink"></param>
        /// <returns></returns>
        BookChapterDto GetBookChapters(string bookLink);

        /// <summary>
        /// 获取小说的内容
        /// </summary>
        /// <param name="chapterLink"></param>
        /// <returns></returns>
        BookContentDto GetBookContent(string chapterLink);
    }
}
