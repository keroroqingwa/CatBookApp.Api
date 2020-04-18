using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatBookApp.BookSearches;
using Microsoft.AspNetCore.Mvc;

namespace WechatMiniProgram.Api.Controllers
{
    /// <summary>
    /// 书本搜索
    /// </summary>
    public class BookSearchController : CatControllerBase
    {
        private readonly IBookSearchAppService _bookSearchAppService;

        public BookSearchController(IBookSearchAppService bookSearchAppService)
        {
            _bookSearchAppService = bookSearchAppService;
        }


        /// <summary>
        /// 获取所有搜索来源分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetBookCategoryList()
        {
            var res = _bookSearchAppService.GetBookCategoryList();

            res.FirstOrDefault().Active = true;

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 搜索书本
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetBooks(string q, int pn, int rule)
        {
            //CheckInterfaceWhiteList();

            var res = _bookSearchAppService.GetBooks(rule, q, pn);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 获取小说章节目录信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetBookChapters(int rule, string bookLink)
        {
            //CheckInterfaceWhiteList();

            var res = _bookSearchAppService.GetBookChapters(rule, bookLink);

            return ActionRes.Success(res);
        }

        /// <summary>
        /// 获取小说的内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionRes GetBookContent(int rule, string chapterLink)
        {
            //CheckInterfaceWhiteList();

            var res = _bookSearchAppService.GetBookContent(rule, chapterLink);

            return ActionRes.Success(res);
        }
    }
}