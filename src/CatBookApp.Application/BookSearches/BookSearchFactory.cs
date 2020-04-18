using Abp.UI;
using CatBookApp.BookSearches.Captures;
using CatBookApp.BookSearches.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookSearches
{
    public class BookSearchFactory
    {
        public IBookCapture CreateBookCaptureService(int rule)
        {
            IBookCapture bookCapture;

            switch (rule)
            {
                case 1:
                    bookCapture = new BiqugeCapture();
                    break;
                case 2:
                    bookCapture = new LiteratureForeignCapture();
                    break;
                default:
                    throw new UserFriendlyException("指定的[rule]没有对应的实现！");
            }

            return bookCapture;
        }
    }
}
