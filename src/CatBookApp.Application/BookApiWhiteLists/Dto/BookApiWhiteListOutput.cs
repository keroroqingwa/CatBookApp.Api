using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatBookApp.BookApiWhiteLists.Dto
{
    [AutoMap(typeof(BookApiWhiteList))]
    public class BookApiWhiteListOutput: EntityDto<long>
    {
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }

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

        ///// <summary>
        ///// 秘钥
        ///// </summary>
        //public string AppSecret { get; set; }
    }
}
