using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatBookApp.BookReadRecords.Dto
{
    public class GetPagedInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 创建者的Openid
        /// </summary>
        //[Required]
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
        //[Required]
        [MaxLength(50)]
        public string BookName { get; set; }
    }
}
