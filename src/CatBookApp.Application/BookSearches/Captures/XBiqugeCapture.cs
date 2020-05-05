using Abp.UI;
using CatBookApp.BookSearches.Dto;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CatBookApp.BookSearches.Captures
{
    /// <summary>
    /// 抓取来源：新笔趣阁 http://www.biquwo.org
    /// </summary>
    public class XBiqugeCapture : IBookCapture
    {
        private string CompleteDomain(string path)
        {
            string domain = "http://www.biquwo.org";

            if (path.StartsWith("http://") || path.StartsWith("https://")) return path;

            if (path.StartsWith("/")) return $"{domain}{path}";

            return $"{domain}/{path}";
        }

        /// <summary>
        /// 搜索书本
        /// </summary>
        /// <param name="q"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public List<BookInfoDto> GetBooks(string q, int pn)
        {
            HtmlDocument doc = new HtmlDocument();
            string url = string.Empty;

            try
            {
                //no.1
                url = $"http://www.biquwo.org/searchbook.php?keyword={q}&page={pn}";
                HtmlWeb webClient = new HtmlWeb();
                webClient.OverrideEncoding = Encoding.UTF8;
                doc = webClient.Load(url);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"抓取网站请求失败，{ex.Message}。请退出后重试");
            }

            List<BookInfoDto> list = new List<BookInfoDto>();
            var books = doc.DocumentNode.SelectNodes("//div[@class='novelslist2']/ul/li");
            if (books != null)
            {
                int i = 0;
                foreach (var item in books)
                {
                    i++;
                    if (i == 1) continue;
                    list.Add(new BookInfoDto()
                    {
                        BookName = item.SelectSingleNode(".//span[@class='s2']").InnerText.Trim(),
                        BookLink = CompleteDomain(item.SelectSingleNode(".//span[@class='s2']//a").Attributes["href"].Value.Trim()),
                        Author = item.SelectSingleNode(".//span[@class='s4']").InnerText.Trim(),
                        CoverImage = "",
                        BookClassify = item.SelectSingleNode(".//span[@class='s1']").InnerText.Trim().Replace("[", "").Replace("]", ""),
                        Last_Update_Time = item.SelectSingleNode(".//span[@class='s6']").InnerText.Trim(),
                        BookIntro = "",
                        Last_Update_ChapterName = item.SelectSingleNode(".//span[@class='s3']").InnerText.Trim(),
                        Last_Update_ChapterLink = CompleteDomain(item.SelectSingleNode(".//span[@class='s3']//a").Attributes["href"].Value.Trim()),
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 根据书本介绍页获取书本信息
        /// </summary>
        /// <param name="bookLink"></param>
        /// <returns></returns>
        public BookChapterDto GetBookChapters(string bookLink)
        {
            HtmlWeb webClient;
            HtmlDocument doc;
            //这里两次请求是为了。。。  嗯，错误请求重试
            try
            {
                try
                {
                    webClient = new HtmlWeb();
                    webClient.OverrideEncoding = Encoding.UTF8;
                    doc = webClient.Load(bookLink);
                }
                catch
                {
                    Thread.Sleep(2000);
                    webClient = new HtmlWeb();
                    webClient.OverrideEncoding = Encoding.UTF8;
                    doc = webClient.Load(bookLink);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"抓取网站请求失败，{ex.Message}。请退出后重试");
            }

            //var _domain = StringHelper.GetUrlDomain(link);
            Uri uri = new Uri(bookLink);
            var _domain = $"{uri.Scheme}://{uri.Host}";
            var nodes = doc.DocumentNode.SelectNodes("//div[@id='info']/p");
            if (nodes == null || nodes.Count == 0) throw new UserFriendlyException("解析网页异常，请重试");

            //章节目录
            List<BookChapterDto.ChapterlistModel> chapterList = new List<BookChapterDto.ChapterlistModel>();
            var chapters = doc.DocumentNode.SelectNodes("//div[@id='list']/dl/dd/a");
            foreach (var item in chapters)
            {
                chapterList.Add(new BookChapterDto.ChapterlistModel()
                {
                    ChapterName = item.InnerText,
                    ChapterLink = _domain + item.Attributes["href"].Value.Trim()
                });
            }

            //书本信息
            var bookChapter = new BookChapterDto()
            {
                BookName = doc.DocumentNode.SelectSingleNode("//div[@id='info']/h1").InnerText.Trim(),
                BookLink = bookLink,
                Author = nodes[0].InnerText.Replace(nodes[0].InnerText.Split('：')[0] + "：", string.Empty).Trim(),
                Status = nodes[1].InnerText.Replace(nodes[1].InnerText.Split('：')[0] + "：", string.Empty).Replace(",加入书架,直达底部", string.Empty),
                Last_Update_Time = nodes[2].InnerText.Replace(nodes[2].InnerText.Split('：')[0] + "：", string.Empty),
                Last_Update_ChapterName = nodes[3].InnerText.Replace(nodes[3].InnerText.Split('：')[0] + "：", string.Empty).Trim(),
                Last_Update_ChapterLink = _domain + nodes[3].ChildNodes["a"].Attributes["href"].Value.Trim(),
                Intro = doc.DocumentNode.SelectSingleNode("//div[@id='intro']").InnerText.Replace("&nbsp;", "").Trim(),
                Chapterlist = chapterList
            };

            return bookChapter;
        }

        /// <summary>
        /// 获取小说内容
        /// </summary>
        /// <param name="chapterLink"></param>
        /// <returns></returns>
        public BookContentDto GetBookContent(string chapterLink)
        {
            HtmlWeb webClient = new HtmlWeb();
            HtmlDocument doc;
            webClient.OverrideEncoding = Encoding.UTF8;

            //这里两次请求是为了。。。 不解释了
            try
            {
                try
                {
                    doc = webClient.Load(chapterLink);
                }
                catch
                {
                    Thread.Sleep(2000);
                    doc = webClient.Load(chapterLink);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"抓取网站请求失败，{ex.Message}。请退出后重试");
            }

            var nodes = doc.DocumentNode.SelectNodes("//div[@class='bookname']/div[@class='bottem1']/a[@href]");
            if (nodes == null || nodes.Count == 0) throw new UserFriendlyException("解析网页异常，请重试");

            //var _domain = StringHelper.GetUrlDomain(link);
            Uri uri = new Uri(chapterLink);
            var _domain = $"{uri.Scheme}://{uri.Host}";
            var _content = doc.DocumentNode.SelectSingleNode("//div[@id='content']").InnerHtml;

            //
            var bookContent = new BookContentDto()
            {
                BookName = doc.DocumentNode.SelectSingleNode("//div[@class='con_top']/a[2]").Attributes["title"].Value.Trim(),
                BookLink = CompleteDomain(doc.DocumentNode.SelectSingleNode("//div[@class='bookname']/div[@class='bottem1']/a[@class='back']").Attributes["href"].Value.Trim()),
                ChapterName = doc.DocumentNode.SelectSingleNode("//div[@class='bookname']/h1").InnerText.Trim(),
                ChapterLink = chapterLink,
                Content = ClearSensitiveCharacter(_content).TrimEnd(),
                NextChapterLink = CompleteDomain(doc.DocumentNode.SelectSingleNode("//div[@class='bookname']/div[@class='bottem1']/a[@class='next']").Attributes["href"].Value.Trim()),
                PrevChapterLink = CompleteDomain(doc.DocumentNode.SelectSingleNode("//div[@class='bookname']/div[@class='bottem1']/a[@class='pre']").Attributes["href"].Value.Trim())
            };
            bookContent.Number_Of_Words = ClearSensitiveCharacter(doc.DocumentNode.SelectSingleNode("//div[@id='content']").InnerText).TrimEnd().Length;

            //有些网站喜欢将最后一章的“下一章的链接地址”设置为返回目录，所以有下面的处理
            var a = chapterLink.Substring(0, chapterLink.LastIndexOf("/")) + "/";
            if (a == bookContent.NextChapterLink)
            {
                bookContent.NextChapterLink = "";
            }

            return bookContent;
        }

        /// <summary>
        /// 过滤敏感字符
        /// </summary>
        /// <returns></returns>
        private string ClearSensitiveCharacter(string str)
        {
            str = str.Replace("[ads:本站换新网址啦，速记方法：，..com]", string.Empty);
            str = str.Replace("xh118", string.Empty);
            str = str.Replace("  ", "　").Replace("&nbsp;&nbsp;", "　");//连续连个英文空格就替换成一个中文空格
            str = str.Replace("&nbsp;", " ").Replace("<br>", "\n").Replace("<br/>", "\n").Replace("<br />", "\n").Replace("readx();", "").Replace("&amp;nbsp;", " ");
            str = CaptureHelper.ClearSensitiveCharacter(str);
            return str;
        }
    }
}
