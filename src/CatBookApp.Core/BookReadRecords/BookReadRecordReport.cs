using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CatBookApp.BookReadRecords
{
    /// <summary>
    /// 书本阅读记录汇总表（不参与具体业务，冗余表）
    /// </summary>
    [Table("book_readrecordreport")]
    public class BookReadRecordReport : Entity<long>, IHasCreationTime, IHasModificationTime
    {
        /// <summary>
        /// 创建者的Openid
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Openid { get; set; }

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
        /// 最近一次阅读的记录（书本记录表Id）
        /// </summary>
        [Required]
        public long LastBookReadRecordId { get; set; }

        
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
