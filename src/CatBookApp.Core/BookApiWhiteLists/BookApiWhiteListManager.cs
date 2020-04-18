using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatBookApp.BookApiWhiteLists
{
    /// <summary>
    /// book相关Api接口白名单 领域服务
    /// </summary>
    public class BookApiWhiteListManager : DomainService
    {
        /// <summary>
        /// book相关Api接口白名单 仓储
        /// </summary>
        private readonly IRepository<BookApiWhiteList, long> _repository;

        public BookApiWhiteListManager(IRepository<BookApiWhiteList, long> repository)
        {
            _repository = repository;
        }

        public async Task<BookApiWhiteList> InsertAsync(BookApiWhiteList entity)
        {
            await CheckDuplicateAppidAsync(null, entity.Appid);

            return await _repository.InsertAsync(entity);
        }

        public async Task<BookApiWhiteList> UpdateAsync(BookApiWhiteList entity)
        {
            await CheckDuplicateAppidAsync(entity.Id, entity.Appid);

            return await _repository.UpdateAsync(entity);
        }

        public async Task CheckDuplicateAppidAsync(long? id, string appid)
        {
            var entity = await _repository.FirstOrDefaultAsync(w => w.Appid == appid);
            if (entity != null)
            {
                if (id == null || id != entity.Id)
                {
                    throw new UserFriendlyException(StringConsts.CheckDuplicateFiledNameException.Replace("{0}", "Appid"));
                }
            }
        }
    }
}
