using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatBookApp.BookApiWhiteLists.Dto
{
    public class GetPagedInput: PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 微信小程序的appid
        /// </summary>
        //[Required]
        [MaxLength(50)]
        public string Appid { get; set; }
    }
}
