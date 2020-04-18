using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CatBookApp.BookApiWhiteLists.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace CatBookApp.BookApiWhiteLists
{
    /// <summary>
    /// book相关Api接口白名单 应用服务层实现
    /// </summary>
    public class BookApiWhiteListAppService : AsyncCrudAppService<BookApiWhiteList, BookApiWhiteListDto, long, PagedResultRequestDto, CreateBookApiWhiteListDto, BookApiWhiteListDto>, IBookApiWhiteListAppService
    {
        /// <summary>
        /// book相关Api接口白名单 仓储对象
        /// </summary>
        private readonly IRepository<BookApiWhiteList, long> _repository;

        /// <summary>
        /// 领域服务
        /// </summary>
        private readonly BookApiWhiteListManager _bookApiWhiteListManager;

        public BookApiWhiteListAppService(IRepository<BookApiWhiteList, long> repository, BookApiWhiteListManager bookApiWhiteListManager) : base(repository)
        {
            _repository = repository;
            _bookApiWhiteListManager = bookApiWhiteListManager;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<BookApiWhiteListDto> CreateAsync(CreateBookApiWhiteListDto input)
        {
            var model = MapToEntity(input);

            await _bookApiWhiteListManager.InsertAsync(model);

            return MapToEntityDto(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<BookApiWhiteListDto> UpdateAsync(BookApiWhiteListDto input)
        {
            var instance = await _repository.GetAsync(input.Id);

            ObjectMapper.Map(input, instance);

            await _bookApiWhiteListManager.UpdateAsync(instance);

            return MapToEntityDto(instance);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        public async Task DeleteAsync(List<long> ids)
        {
            await _repository.DeleteAsync(w => ids.Contains(w.Id));
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<BookApiWhiteListOutput>> GetPagedAsync(GetPagedInput input)
        {
            //过滤查询
            var query = _repository.GetAll()
                .WhereIf(!input.Appid.IsNullOrEmpty(), t => t.Appid == input.Appid);

            //排序
            query = !string.IsNullOrEmpty(input.Sorting) ? query.OrderBy(input.Sorting) : query.OrderByDescending(t => t.CreationTime);

            //获取总数
            var count = await query.CountAsync();

            //列表数据
            var list = query.PageBy(input).ToList();

            return new PagedResultDto<BookApiWhiteListOutput>(count, ObjectMapper.Map<List<BookApiWhiteListOutput>>(list));
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        public async Task<BookApiWhiteListOutput> GetByAppidAsync(string appid)
        {
            var entity = await _repository.FirstOrDefaultAsync(w => w.Appid == appid);

            return ObjectMapper.Map<BookApiWhiteListOutput>(entity);
        }
    }
}
