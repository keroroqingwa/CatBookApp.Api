using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatBookApp.BookUsers.Dto
{
    [AutoMap(typeof(BookUser), typeof(BookUserOutput))]
    public class UpdateBookUserDto : EntityDto<long>
    {
        /// <summary>
        /// Openid
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Openid { get; set; }

        /// <summary>
        /// NickName
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string NickName { get; set; }

        /// <summary>
        /// AvatarUrl
        /// </summary>
        [MaxLength(500)]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [MaxLength(10)]
        public string Gender { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [MaxLength(50)]
        public string Country { get; set; }

        /// <summary>
        /// Province
        /// </summary>
        [MaxLength(50)]
        public string Province { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [MaxLength(50)]
        public string City { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        [MaxLength(50)]
        public string Language { get; set; }
    }
}
