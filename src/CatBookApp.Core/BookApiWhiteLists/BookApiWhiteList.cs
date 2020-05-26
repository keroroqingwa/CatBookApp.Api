using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CatBookApp.BookApiWhiteLists
{
    /// <summary>
    /// book相关Api接口白名单
    /// </summary>
    [Table("book_apiwhitelist")]
    public class BookApiWhiteList: FullAuditedEntity<long>
    {
        /// <summary>
        /// 微信小程序的appid
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Appid { get; set; }

        /// <summary>
        /// 白名单的过期时间
        /// </summary>
        [Required]
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 秘钥
        /// </summary>
        [MaxLength(100)]
        public string AppSecret { get; set; }
    }
}
