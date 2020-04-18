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
    /// 抓取来源：笔趣阁
    /// </summary>
    public class BiqugeCapture : IBookCapture
    {
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
            //这里请求三次是因为。。。 调试过就知道，你就当做是错误重试吧 (′゜ω。‵)
            try
            {
                try
                {
                    //no.1
                    url = $"https://www.xxbiquge.com/search.php?keyword={q}&page={pn}&p={pn - 1}";
                    HtmlWeb webClient = new HtmlWeb();
                    doc = webClient.Load(url);
                }
                catch
                {
                    try
                    {
                        //no.2
                        url = $"http://zhannei.baidu.com/cse/search?s=8823758711381329060&q={q}&page={pn}&p={pn - 1}";
                        Thread.Sleep(1000 * 1);
                        var html = Utils.HttpHelper.Get(url);
                        doc.LoadHtml(html);
                    }
                    catch
                    {
                        //no.3
                        url = $"http://zhannei.baidu.com/cse/search?s=3654077655350271938&q={q}&page={pn}&p={pn - 1}";
                        Thread.Sleep(1000 * 2);
                        HtmlWeb webClient = new HtmlWeb();
                        doc = webClient.Load(url);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException($"抓取网站请求失败，{ex.Message}。请退出后重试");
            }

            List<BookInfoDto> list = new List<BookInfoDto>();
            var books = doc.DocumentNode.SelectNodes("//div[@class='result-list']/div");
            if (books != null)
            {
                int i = 0;
                foreach (var item in books)
                {
                    list.Add(new BookInfoDto()
                    {
                        BookName = item.SelectNodes("//a[@class='result-game-item-title-link']")[i].Attributes["title"].Value,
                        BookLink = item.SelectNodes("//a[@class='result-game-item-title-link']")[i].Attributes["href"].Value.Trim(),
                        Author = item.SelectNodes("//p[@class='result-game-item-info-tag']")[i * 4 + 0].SelectNodes("span")[1].InnerText.Replace("\r\n", string.Empty).Trim(),
                        CoverImage = item.SelectNodes("//img[@class='result-game-item-pic-link-img']")[i].Attributes["src"].Value,
                        BookClassify = item.SelectNodes("//p[@class='result-game-item-info-tag']")[i * 4 + 1].SelectNodes("span")[1].InnerText.Replace("\r\n", string.Empty).Trim(),
                        Last_Update_Time = item.SelectNodes("//p[@class='result-game-item-info-tag']")[i * 4 + 2].SelectNodes("span")[1].InnerText.Replace("\r\n", string.Empty).Trim(),
                        BookIntro = item.SelectNodes("//p[@class='result-game-item-desc']")[i].InnerText,
                        Last_Update_ChapterName = item.SelectNodes("//p[@class='result-game-item-info-tag']")[i * 4 + 3].SelectSingleNode("a").InnerText.Trim(),
                        Last_Update_ChapterLink = item.SelectNodes("//p[@class='result-game-item-info-tag']")[i * 4 + 3].SelectSingleNode("a").Attributes["href"].Value.Trim()
                    });
                    i++;
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
                BookName = doc.DocumentNode.SelectSingleNode("//div[@class='footer_cont']/p/a").InnerText.Trim(),
                BookLink = _domain + nodes[1].Attributes["href"].Value.Trim(),
                ChapterName = doc.DocumentNode.SelectSingleNode("//div[@class='bookname']/h1").InnerText.Trim(),
                ChapterLink = chapterLink,
                Content = ClearSensitiveCharacter(_content).TrimEnd(),
                NextChapterLink = _domain + nodes[2].Attributes["href"].Value.Trim(),
                PrevChapterLink = _domain + nodes[0].Attributes["href"].Value.Trim()
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
            str = str.Replace("  ", "　").Replace("&nbsp;&nbsp;", "　");//连续连个英文空格就替换成一个中文空格
            str = str.Replace("&nbsp;", " ").Replace("<br>", "\n").Replace("<br/>", "\n").Replace("<br />", "\n").Replace("readx();", "").Replace("&amp;nbsp;", " ");
            str = CaptureHelper.ClearSensitiveCharacter(str);
            return str;
        }
    }
}
