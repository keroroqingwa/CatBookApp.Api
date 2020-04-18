using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatBookApp.BookUserPreferences.Dto
{
    public class GetPagedInput : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// Openid
        /// </summary>
        //[Required]
        [MaxLength(100)]
        public string Openid { get; set; }
    }
}
