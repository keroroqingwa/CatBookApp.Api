using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CatBookApp.BookUserMessages
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    [Table("Book_UserMessage")]
    public class BookUserMessage : FullAuditedEntity<long>
    {
        /// <summary>
        /// 不填写则默认发送给所有人，填写则指定openid发送
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Openid { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength(200)]
        public string Content { get; set; }
    }
}
