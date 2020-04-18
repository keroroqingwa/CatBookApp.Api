using Abp.EntityFrameworkCore;
using CatBookApp.BookApiWhiteLists;
using CatBookApp.BookReadRecords;
using CatBookApp.BookUserPreferences;
using CatBookApp.BookUsers;
using Microsoft.EntityFrameworkCore;

namespace CatBookApp.EntityFrameworkCore
{
    public class CatBookAppDbContext : AbpDbContext
    {
        /// <summary>
        /// Book用户表
        /// </summary>
        public virtual DbSet<BookUser> BookUser { get; set; }

        ///// <summary>
        ///// 用户信息表
        ///// </summary>
        //public virtual DbSet<BookUserMessage> BookUserMessage { get; set; }

        ///// <summary>
        ///// 用户已读信息记录表
        ///// </summary>
        //public virtual DbSet<BookUserMessageReadRecord> BookUserMessageReadRecord { get; set; }

        /// <summary>
        /// 用户偏好表
        /// </summary>
        public virtual DbSet<BookUserPreference> BookUserPreference { get; set; }

        /// <summary>
        /// 书本阅读记录表
        /// </summary>
        public virtual DbSet<BookReadRecord> BookReadRecord { get; set; }

        /// <summary>
        /// 书本阅读记录汇总表
        /// </summary>
        public virtual DbSet<BookReadRecordReport> BookReadRecordReport { get; set; }

        /// <summary>
        /// book相关Api接口白名单
        /// </summary>
        public virtual DbSet<BookApiWhiteList> BookApiWhiteList { get; set; }


        public CatBookAppDbContext(DbContextOptions<CatBookAppDbContext> options)
            : base(options)
        {

        }
    }
}
