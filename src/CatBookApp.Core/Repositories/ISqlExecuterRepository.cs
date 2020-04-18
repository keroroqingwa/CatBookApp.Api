using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CatBookApp.Repositories
{
    public interface ISqlExecuterRepository
    {
        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>
        /// <returns>执行命令后由数据库返回的结果</returns>
        Task<int> ExecuteAsync(string sql, params object[] parameters);

        /// <summary>
        /// 执行sql语句，返回指定类型的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        List<T> ExecuteQuery<T>(string sql);
    }
}
