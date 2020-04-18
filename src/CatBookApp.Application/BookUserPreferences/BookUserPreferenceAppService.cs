using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CatBookApp.BookUserPreferences.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace CatBookApp.BookUserPreferences
{
    /// <summary>
    /// 用户偏好 应用服务层实现
    /// </summary>
    public class BookUserPreferenceAppService : AsyncCrudAppService<BookUserPreference, BookUserPreferenceDto, long, PagedResultRequestDto, CreateOrUpdateDto, BookUserPreferenceDto>, IBookUserPreferenceAppService
    {
        /// <summary>
        /// 用户偏好 仓储对象
        /// </summary>
        private readonly IRepository<BookUserPreference, long> _repository;

        public BookUserPreferenceAppService(IRepository<BookUserPreference, long> repository) : base(repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BookUserPreferenceDto> CreateOrUpdateAsync(CreateOrUpdateDto input)
        {
            var entity = await _repository.FirstOrDefaultAsync(w => w.Openid == input.Openid);

            if (entity == null)
            {
                entity = MapToEntity(input);
                await _repository.InsertAsync(entity);
            }
            else
            {
                ObjectMapper.Map(input, entity);
                await _repository.UpdateAsync(entity);
            }

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public async Task<BookUserPreferenceDto> GetByOpenidAsync(string openid)
        {
            var entity = await _repository.FirstOrDefaultAsync(w => w.Openid == openid);

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<BookUserPreferenceOutput>> GetPagedAsync(GetPagedInput input)
        {
            //过滤查询
            var query = _repository.GetAll()
                .WhereIf(!input.Openid.IsNullOrEmpty(), t => t.Openid == input.Openid);

            //排序
            query = !string.IsNullOrEmpty(input.Sorting) ? query.OrderBy(input.Sorting) : query.OrderByDescending(t => t.CreationTime);

            //获取总数
            var count = await query.CountAsync();

            //列表数据
            var list = query.PageBy(input).ToList();

            return new PagedResultDto<BookUserPreferenceOutput>(count, ObjectMapper.Map<List<BookUserPreferenceOutput>>(list));
        }
    }
}
