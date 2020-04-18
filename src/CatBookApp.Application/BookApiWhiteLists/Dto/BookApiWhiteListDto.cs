using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatBookApp.BookApiWhiteLists.Dto
{
    /// <summary>
    /// book相关Api接口白名单 DTO
    /// </summary>
    [AutoMap(typeof(BookApiWhiteList))]
    public class BookApiWhiteListDto : EntityDto<long>
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
    }
}
