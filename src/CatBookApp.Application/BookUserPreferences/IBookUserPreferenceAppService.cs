using Abp.Application.Services.Dto;
using CatBookApp.BookUserPreferences.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatBookApp.BookUserPreferences
{
    public interface IBookUserPreferenceAppService
    {
        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookUserPreferenceDto> CreateOrUpdateAsync(CreateOrUpdateDto input);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        Task<BookUserPreferenceDto> GetByOpenidAsync(string openid);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BookUserPreferenceOutput>> GetPagedAsync(GetPagedInput input);
    }
}
