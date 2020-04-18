using System;
using System.Threading.Tasks;
using Abp.TestBase;
using CatBookApp.EntityFrameworkCore;
using CatBookApp.Tests.TestDatas;

namespace CatBookApp.Tests
{
    public class CatBookAppTestBase : AbpIntegratedTestBase<CatBookAppTestModule>
    {
        public CatBookAppTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<CatBookAppDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<CatBookAppDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<CatBookAppDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<CatBookAppDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<CatBookAppDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<CatBookAppDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<CatBookAppDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<CatBookAppDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}
