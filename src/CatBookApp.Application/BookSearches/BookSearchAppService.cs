using Abp.Application.Services;
using CatBookApp.BookSearches.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookSearches
{
    /// <summary>
    /// 书本搜索服务 应用服务层实现
    /// </summary>
    public class BookSearchAppService : ApplicationService, IBookSearchAppService
    {
        /// <summary>
        /// 获取所有搜索来源分类
        /// </summary>
        /// <returns></returns>
        public List<GetBookCategoryListOutput> GetBookCategoryList()
        {
            var list = new List<GetBookCategoryListOutput>()
                {
                    new GetBookCategoryListOutput(){ Value = 1,Key = "笔趣阁" },
                    new GetBookCategoryListOutput(){ Value = 2,Key = "外国文学" },
                    //new GetBookCategoryListOutput(){ Value = 3,Key = "全本小说网" },
                    //new GetBookCategoryListOutput(){ Value = 4,Key = "书旗网" },
                    //new GetBookCategoryListOutput(){ Value = 5,Key = "言情小说吧" },
                    //new GetBookCategoryListOutput(){ Value = 6,Key = "古典文学网" }
                };
            return list;
        }

        private Captures.IBookCapture CreateBookCaptureService(int rule)
        {
            var service = new BookSearchFactory().CreateBookCaptureService(rule);
            return service;
        }

        /// <summary>
        /// 搜索书本
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="q"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public List<BookInfoDto> GetBooks(int rule, string q, int pn)
        {
            var service = CreateBookCaptureService(rule);
            var data = service.GetBooks(q, pn);
            return data;
        }

        /// <summary>
        /// 获取小说章节目录信息
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="bookLink"></param>
        /// <returns></returns>
        public BookChapterDto GetBookChapters(int rule, string bookLink)
        {
            var service = CreateBookCaptureService(rule);
            var data = service.GetBookChapters(bookLink);
            return data;
        }

        /// <summary>
        /// 获取小说的内容
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="chapterLink"></param>
        /// <returns></returns>
        public BookContentDto GetBookContent(int rule, string chapterLink)
        {
            var service = CreateBookCaptureService(rule);
            var data = service.GetBookContent(chapterLink);
            return data;
        }
    }
}
