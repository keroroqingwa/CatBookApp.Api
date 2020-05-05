using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CatBookApp.BookSearches.Captures
{
    public class CaptureHelper
    {
        private static Regex GetRegex(string pattern)
        {
            return new Regex(
                pattern,
                RegexOptions.IgnoreCase
                | RegexOptions.CultureInvariant
                | RegexOptions.IgnorePatternWhitespace
                | RegexOptions.Compiled
            );
        }

        /// <summary>
        /// 过滤敏感字符
        /// </summary>
        /// <returns></returns>
        public static string ClearSensitiveCharacter(string input)
        {
            //过滤 www.???.com
            MatchCollection ms = GetRegex("www\\..*?\\.com").Matches(input);
            foreach (var item in ms)
            {
                input = input.Replace(item.ToString(), "");
            }
            //
            input = HtmlToTxt(input);

            return input;
        }

        /// <summary>
        /// 对字符串进行检查和替换其中的特殊字符
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
                        @"<script[^>]*?>.*?</script>",
                        @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                        //@"([\r\n])[\s]+",
                        @"&(quot|#34);",
                        @"&(amp|#38);",
                        @"&(lt|#60);",
                        @"&(gt|#62);",
                        @"&(nbsp|#160);",
                        @"&(iexcl|#161);",
                        @"&(cent|#162);",
                        @"&(pound|#163);",
                        @"&(copy|#169);",
                        @"&#(\d+);",
                        @"-->",
                        @"<!--.*\n"
                        };

            //string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }
    }
}
