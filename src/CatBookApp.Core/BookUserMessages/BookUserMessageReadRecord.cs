using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CatBookApp.BookUserMessages
{
    /// <summary>
    /// 用户已读信息记录表
    /// </summary>
    [Table("Book_UserMessageReadRecord")]
    public class BookUserMessageReadRecord: CreationAuditedEntity<long>
    {
        /// <summary>
        /// 用户信息表id
        /// </summary>
        [Required]
        public long BookUserMessageId { get; set; }

        /// <summary>
        /// Openid
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Openid { get; set; }
    }
}
