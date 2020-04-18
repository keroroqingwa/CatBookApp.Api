using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CatBookApp.BookUsers
{
    /// <summary>
    /// Book用户表
    /// </summary>
    [Table("Book_User")]
    public class BookUser: AuditedEntity<long>
    {
        /// <summary>
        /// 微信小程序的appid
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Appid { get; set; }

        /// <summary>
        /// 用户id（mm开头+递增的用户总数）
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string UserId { get; set; }

        /// <summary>
        /// Openid
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Openid { get; set; }

        /// <summary>
        /// NickName
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string NickName { get; set; }

        /// <summary>
        /// AvatarUrl
        /// </summary>
        [MaxLength(500)]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [MaxLength(10)]
        public string Gender { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [MaxLength(50)]
        public string Country { get; set; }

        /// <summary>
        /// Province
        /// </summary>
        [MaxLength(50)]
        public string Province { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [MaxLength(50)]
        public string City { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        [MaxLength(50)]
        public string Language { get; set; }

        /// <summary>
        /// 阅读时长（分钟）
        /// </summary>
        public int ReadMinutes { get; set; }
    }
}
