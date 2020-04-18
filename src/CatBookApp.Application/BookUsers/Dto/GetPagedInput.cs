using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatBookApp.BookUsers.Dto
{
    public class GetPagedInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 微信小程序的appid
        /// </summary>
        //[Required]
        [MaxLength(50)]
        public string Appid { get; set; }

        /// <summary>
        /// 用户id（mm开头+递增的用户总数）
        /// </summary>
        //[Required]
        [MaxLength(10)]
        public string UserId { get; set; }

        /// <summary>
        /// Openid
        /// </summary>
        //[Required]
        [MaxLength(100)]
        public string Openid { get; set; }

        /// <summary>
        /// NickName
        /// </summary>
        //[Required]
        [MaxLength(50)]
        public string NickName { get; set; }
    }
}
