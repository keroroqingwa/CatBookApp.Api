using Abp.AutoMapper;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatBookApp.BookReadRecords.Dto
{
    [AutoMap(typeof(BookReadRecord))]
    public class LastReadingOutput : Entity<long>
    {
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

        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
