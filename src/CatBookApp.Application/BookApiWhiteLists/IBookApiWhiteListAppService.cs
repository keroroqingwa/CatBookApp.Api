using Abp.Application.Services.Dto;
using CatBookApp.BookApiWhiteLists.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatBookApp.BookApiWhiteLists
{
    /// <summary>
    /// book相关Api接口白名单 应用服务层接口
    /// </summary>
    public interface IBookApiWhiteListAppService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookApiWhiteListDto> CreateAsync(CreateBookApiWhiteListDto input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookApiWhiteListDto> UpdateAsync(BookApiWhiteListDto input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        Task DeleteAsync(List<long> ids);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BookApiWhiteListOutput>> GetPagedAsync(GetPagedInput input);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        Task<BookApiWhiteListOutput> GetByAppidAsync(string appid);
    }
}
