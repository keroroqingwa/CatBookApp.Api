using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CatBookApp.BookUsers.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace CatBookApp.BookUsers
{
    /// <summary>
    /// Book用户表 应用服务层实现
    /// </summary>
    public class BookUserAppService : AsyncCrudAppService<BookUser, BookUserDto, long, PagedResultRequestDto, CreateBookUserDto, UpdateBookUserDto>, IBookUserAppService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Book用户表 仓储对象
        /// </summary>
        private readonly IRepository<BookUser, long> _repository;

        ///// <summary>
        ///// Book用户表 领域服务
        ///// </summary>
        //private readonly BookUserManager _bookUserManager;

        public BookUserAppService(IRepository<BookUser, long> repository/*, BookUserManager bookUserManager*/)
            : base(repository)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"Configs{Path.DirectorySeparatorChar}wechatsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();

            _repository = repository;
            //_bookUserManager = bookUserManager;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<BookUserDto> CreateAsync(CreateBookUserDto input)
        {
            int count = _repository.Count();

            var model = new BookUser();
            model.Appid = _configuration["Book:Appid"].ToString();
            model.UserId = $"mm{count + 10000}";

            ObjectMapper.Map(input, model);

            await _repository.InsertAsync(model);

            return MapToEntityDto(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<BookUserDto> UpdateAsync(UpdateBookUserDto input)
        {
            var instance = await _repository.FirstOrDefaultAsync(w => w.Openid == input.Openid);

            ObjectMapper.Map(input, instance);

            await _repository.UpdateAsync(instance);

            return MapToEntityDto(instance);
        }

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="ids"></param>
        //public async Task DeleteAsync(List<long> ids)
        //{
        //    await _repository.DeleteAsync(w => ids.Contains(w.Id));
        //}

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BookUserOutput> GetByOpenidAsync(string openid)
        {
            var entity = await _repository.FirstOrDefaultAsync(a => a.Openid == openid);
            return ObjectMapper.Map<BookUserOutput>(entity);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<BookUserOutput>> GetPagedAsync(GetPagedInput input)
        {
            //过滤查询
            var query = _repository.GetAll()
                .WhereIf(!input.Appid.IsNullOrEmpty(), t => t.Appid == input.Appid)
                .WhereIf(!input.UserId.IsNullOrEmpty(), t => t.UserId == input.UserId)
                .WhereIf(!input.Openid.IsNullOrEmpty(), t => t.Openid == input.Openid)
                .WhereIf(!input.NickName.IsNullOrEmpty(), t => t.NickName.Contains(input.NickName));

            //排序
            query = !string.IsNullOrEmpty(input.Sorting) ? query.OrderBy(input.Sorting) : query.OrderByDescending(t => t.CreationTime);

            //获取总数
            var count = await query.CountAsync();

            //列表数据
            var list = query.PageBy(input).ToList();

            return new PagedResultDto<BookUserOutput>(count, ObjectMapper.Map<List<BookUserOutput>>(list));
        }
    }
}
