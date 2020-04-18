using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp
{
    public class StringConsts
    {
        /// <summary>
        /// 检验数据表中字段值重复时抛出的异常信息
        /// </summary>
        public const string CheckDuplicateFiledNameException = "字段“{0}”的值在数据库中已存在，不能设置重复的值";
    }
}
