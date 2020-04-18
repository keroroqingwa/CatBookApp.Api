using CatBookApp.BookSearches.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookSearches
{
    /// <summary>
    /// 书本搜索服务 应用服务层接口
    /// </summary>
    public interface IBookSearchAppService
    {
        /// <summary>
        /// 获取所有搜索来源分类
        /// </summary>
        /// <returns></returns>
        List<GetBookCategoryListOutput> GetBookCategoryList();

        /// <summary>
        /// 搜索书本
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="q"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        List<BookInfoDto> GetBooks(int rule, string q, int pn);

        /// <summary>
        /// 获取小说章节目录信息
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="bookLink"></param>
        /// <returns></returns>
        BookChapterDto GetBookChapters(int rule, string bookLink);

        /// <summary>
        /// 获取小说的内容
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="chapterLink"></param>
        /// <returns></returns>
        BookContentDto GetBookContent(int rule, string chapterLink);
    }
}
