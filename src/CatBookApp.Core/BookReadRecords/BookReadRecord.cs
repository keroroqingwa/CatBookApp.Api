using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CatBookApp.BookReadRecords
{
    /// <summary>
    /// 书本阅读记录表
    /// </summary>
    [Table("Book_ReadRecord")]
    public class BookReadRecord : AuditedEntity<long>
    {
        /// <summary>
        /// 创建者的Openid
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Openid { get; set; }

        /// <summary>
        /// 搜索来源
        /// </summary>
        public int Rule { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [MaxLength(50)]
        public string Author { get; set; }

        /// <summary>
        /// 小说名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string BookName { get; set; }

        /// <summary>
        /// 小说分类
        /// </summary>
        [MaxLength(50)]
        public string BookClassify { get; set; }

        /// <summary>
        /// 小说介绍页链接地址
        /// </summary>
        [MaxLength(200)]
        public string BookLink { get; set; }

        /// <summary>
        /// 小说封面图片
        /// </summary>
        [MaxLength(500)]
        public string CoverImage { get; set; }

        /// <summary>
        /// 章节名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ChapterName { get; set; }

        /// <summary>
        /// 小说章节页链接地址
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string ChapterLink { get; set; }

        /// <summary>
        /// 当前章节的字数
        /// </summary>
        public int NumberOfWords { get; set; }

        /// <summary>
        /// 阅读时长（秒）
        /// </summary>
        public int ReadSeconds { get; set; }
    }
}
