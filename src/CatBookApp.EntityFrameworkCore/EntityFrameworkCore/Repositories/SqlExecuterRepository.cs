using Abp.Dependency;
using Abp.EntityFrameworkCore;
using CatBookApp.Configuration;
using CatBookApp.EntityFrameworkCore;
using CatBookApp.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatBookApp.Repositories
{
    public class SqlExecuterRepository : ISqlExecuterRepository, ITransientDependency
    {
        private readonly IDbContextProvider<CatBookAppDbContext> _dbContextProvider;

        private readonly IConfiguration _configuration;

        public SqlExecuterRepository(IDbContextProvider<CatBookAppDbContext> dbContextProvider, IConfiguration configuration)
        {
            _dbContextProvider = dbContextProvider;
            _configuration = configuration;
        }

        /// <summary>
        /// 执行给定的命令
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">要应用于命令字符串的参数</param>
        /// <returns>执行命令后由数据库返回的结果</returns>
        public async Task<int> ExecuteAsync(string sql, params object[] parameters)
        {
            //return await _dbContextProvider.GetDbContext().Database.ExecuteSqlCommandAsync(sql, parameters);
            return await _dbContextProvider.GetDbContext().Database.ExecuteSqlRawAsync(sql, parameters);

        }

        /// <summary>
        /// 执行sql语句，返回指定类型的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> ExecuteQuery<T>(string sql)
        {
            var ds = Query(sql);
            return DataSetToList<T>(ds, 0);
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataSet</returns>
        private DataSet Query(string sql)
        {
            //var _configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            var connectionString = _configuration.GetConnectionString(CatBookAppConsts.ConnectionStringName);
            //var connectionString = _dbContextProvider.GetDbContext().Database.GetDbConnection().ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    MySqlDataAdapter command = new MySqlDataAdapter(sql, connection);
                    command.Fill(ds);
                }
                catch (MySqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        /// <summary>
        /// DataSetToList
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="ds">一个DataSet实例，也就是数据源</param>
        /// <param name="tableIndext">DataSet容器里table的下标，只有用于取得哪个table，也就是需要转换表的索引</param>
        /// <returns></returns>
        private List<T> DataSetToList<T>(DataSet ds, int tableIndext)
        {
            //确认参数有效
            if (ds == null || ds.Tables.Count <= 0 || tableIndext < 0)
            {
                return null;
            }

            DataTable dt = ds.Tables[tableIndext];
            IList<T> list = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = Activator.CreateInstance<T>();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo p in properties)
                    {
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(p.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                p.SetValue(t, Convert.ChangeType(dt.Rows[i][j], p.PropertyType), null);
                            }
                            else
                            {
                                p.SetValue(t, null, null);
                            }
                            break;
                        }
                    }
                }

                list.Add(t);
            }
            return list.ToList();

        }
    }
}
