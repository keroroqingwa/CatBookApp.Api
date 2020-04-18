using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookUsers
{
    /// <summary>
    /// Book用户表 领域服务
    /// </summary>
    public class BookUserManager : DomainService
    {
        /// <summary>
        /// Book用户表 仓储
        /// </summary>
        private readonly IRepository<BookUser, long> _repository;

        public BookUserManager(IRepository<BookUser, long> repository)
        {
            _repository = repository;
        }
    }
}
