using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatBookApp.BookReadRecords.Dto
{
    public class GetPagedByLastReadingInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 创建者的Openid
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Openid { get; set; }
    }
}
