using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CatBookApp.BookUserPreferences.Dto
{
    /// <summary>
    /// 用户偏好 DTO
    /// </summary>
    [AutoMap(typeof(BookUserPreference))]
    public class BookUserPreferenceDto : EntityDto<long>
    {
        /// <summary>
        /// Openid
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Openid { get; set; }

        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [MaxLength(10)]
        public string FontColor { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        [MaxLength(10)]
        public string BackgroundColor { get; set; }

        /// <summary>
        /// 字体
        /// </summary>
        [MaxLength(50)]
        public string FontFamily { get; set; }

        ///// <summary>
        ///// 屏幕亮度（0~10），0最暗，10最亮
        ///// </summary>
        //public int ScreenBrightness { get; set; }

        /// <summary>
        /// 是否屏幕常亮
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? KeepScreenOn { get; set; }
    }
}
