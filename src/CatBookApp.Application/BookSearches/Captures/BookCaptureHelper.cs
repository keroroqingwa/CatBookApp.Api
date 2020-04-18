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

            return input;
        }
    }
}
