using Abp.Application.Services.Dto;
using CatBookApp.BookUsers.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatBookApp.BookUsers
{
    /// <summary>
    /// Book用户表 应用服务层接口
    /// </summary>
    public interface IBookUserAppService
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookUserDto> CreateAsync(CreateBookUserDto input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BookUserDto> UpdateAsync(UpdateBookUserDto input);

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="ids"></param>
        //Task DeleteAsync(List<long> ids);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BookUserOutput> GetByOpenidAsync(string openid);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BookUserOutput>> GetPagedAsync(GetPagedInput input);
    }
}
