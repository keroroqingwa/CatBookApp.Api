using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CatBookApp.BookReadRecords.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Extensions;
//using CatBookApp.BookChapterReadRecords;

namespace CatBookApp.BookReadRecords
{
    /// <summary>
    /// 书本阅读记录表 应用服务层实现
    /// </summary>
    public class BookReadRecordAppService : AsyncCrudAppService<BookReadRecord, BookReadRecordDto, long, PagedResultRequestDto, CreateBookReadRecordDto, BookReadRecordDto>, IBookReadRecordAppService
    {
        /// <summary>
        /// 书本阅读记录表 仓储对象
        /// </summary>
        private readonly IRepository<BookReadRecord, long> _repository;

        /// <summary>
        /// 书本阅读记录汇总表 仓储对象
        /// </summary>
        private readonly IRepository<BookReadRecordReport, long> _repositoryReport;

        public BookReadRecordAppService(IRepository<BookReadRecord, long> repository, IRepository<BookReadRecordReport, long> repositoryReport) : base(repository)
        {
            _repository = repository;
            _repositoryReport = repositoryReport;
        }


        /// <summary>
        /// 新增或更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BookReadRecordDto> CreateOrUpdateAsync(CreateBookReadRecordDto input)
        {
            var entity = await _repository.FirstOrDefaultAsync(w => w.Openid == input.Openid && w.ChapterLink == input.ChapterLink);

            if (entity == null)
            {
                entity = MapToEntity(input);
                var id = await _repository.InsertAndGetIdAsync(entity);
                entity.Id = id;
            }
            else
            {
                ObjectMapper.Map(input, entity);
                await _repository.UpdateAsync(entity);
            }

            //
            await CreateOrUpdateReport(entity);

            return MapToEntityDto(entity);
        }

        /// <summary>
        /// 新增或更新“书本阅读记录汇总表”
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isAdd"></param>
        private async Task CreateOrUpdateReport(BookReadRecord model)
        {
            var entity = await _repositoryReport.GetAll().AsNoTracking().FirstOrDefaultAsync(w => w.Author == model.Author && w.BookName == model.BookName && w.Openid == model.Openid);

            var obj = new BookReadRecordReport()
            {
                Openid = model.Openid,
                Author = model.Author,
                BookName = model.BookName,
                LastBookReadRecordId = model.Id,
                LastModificationTime = DateTime.Now,
                IsHideByHomePage = false
            };

            if (entity == null)
            {
                await _repositoryReport.InsertAsync(obj);
            }
            else
            {
                obj.Id = entity.Id;
                obj.CreationTime = entity.CreationTime;
                await _repositoryReport.UpdateAsync(obj);
            }
        }

        /// <summary>
        /// 分页获取用户最近的阅读记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<LastReadingOutput>> GetPagedByLastReadingAsync(GetPagedByLastReadingInput input)
        {
            //过滤查询
            var query = _repositoryReport.GetAll()
                .WhereIf(!string.IsNullOrEmpty(input.Openid), t => t.Openid == input.Openid)
                .WhereIf(input.IsHideByHomePage != null, t => t.IsHideByHomePage == input.IsHideByHomePage);

            //排序
            query = !string.IsNullOrEmpty(input.Sorting) ? query.OrderBy(input.Sorting) : query.OrderByDescending(t => t.LastModificationTime);

            //获取总数
            var count = await query.CountAsync();

            //列表数据
            var list = query.PageBy(input).ToList();

            var lastBookReadRecordIds = list.Select(s => s.LastBookReadRecordId).ToList();

            var listRecords = _repository.GetAllList(w => lastBookReadRecordIds.Contains(w.Id));
            listRecords = listRecords.OrderBy(a => Array.IndexOf(lastBookReadRecordIds.ToArray(), a.Id)).ToList(); //排序

            var resList = ObjectMapper.Map<List<LastReadingOutput>>(listRecords);
            foreach(var item in resList)
            {
                item.ReportId = list.FirstOrDefault(w => w.LastBookReadRecordId == item.Id).Id;
            }

            return new PagedResultDto<LastReadingOutput>(count, resList);
        }

        /// <summary>
        /// 更新阅读时长
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public async Task UpdateDurationAsync(long id, int seconds)
        {
            var entity = await _repository.GetAsync(id);
            entity.ReadSeconds += seconds;

            await _repository.UpdateAsync(entity);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BookReadRecordOutput> GetAsync(long id)
        {
            var entity = await _repository.GetAsync(id);

            return ObjectMapper.Map<BookReadRecordOutput>(entity);
        }

        /// <summary>
        /// 书本阅读记录汇总信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns>已阅读书本数、章节数、时长(min)</returns>
        public async Task<BookReadRecoredStatisticOutput> GetBookReadRecordSummaryAsync(string openid)
        {
            //书本数
            var bookCount = await _repositoryReport.CountAsync(w => w.Openid == openid);
            //章节数
            var chapterCount = await _repository.CountAsync(w => w.Openid == openid);
            //时长
            var readSeconds = _repository.GetAll().Where(w => w.Openid == openid).Sum(s => s.ReadSeconds);
            var readMinutes = readSeconds / 60 + (readSeconds % 60 > 0 ? 1 : 0);

            return new BookReadRecoredStatisticOutput()
            {
                BookCount = bookCount,
                ChapterCount = chapterCount,
                ReadMinutes = readMinutes
            };
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<BookReadRecordOutput>> GetPagedAsync(GetPagedInput input)
        {
            //过滤查询
            var query = _repository.GetAll()
                .WhereIf(!input.Openid.IsNullOrEmpty(), t => t.Openid == input.Openid)
                .WhereIf(!input.Author.IsNullOrEmpty(), t => t.Author.Contains(input.Author))
                .WhereIf(!input.BookName.IsNullOrEmpty(), t => t.BookName.Contains(input.BookName));

            //排序
            query = !string.IsNullOrEmpty(input.Sorting) ? query.OrderBy(input.Sorting) : query.OrderByDescending(t => t.CreationTime);

            //获取总数
            var count = await query.CountAsync();

            //列表数据
            var list = query.PageBy(input).ToList();

            return new PagedResultDto<BookReadRecordOutput>(count, ObjectMapper.Map<List<BookReadRecordOutput>>(list));
        }

        /// <summary>
        /// 设置在小程序首页中 隐藏/显示 当前阅读记录
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="isHide"></param>
        /// <returns></returns>
        public async Task SetHideByHomePage(long reportId, bool isHide)
        {
            var entity = await _repositoryReport.FirstOrDefaultAsync(reportId);

            entity.IsHideByHomePage = isHide;

            await _repositoryReport.UpdateAsync(entity);
        }
    }
}
