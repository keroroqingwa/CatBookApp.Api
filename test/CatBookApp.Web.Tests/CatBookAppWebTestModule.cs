using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CatBookApp.Web.Startup;
namespace CatBookApp.Web.Tests
{
    [DependsOn(
        typeof(CatBookAppWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class CatBookAppWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CatBookAppWebTestModule).GetAssembly());
        }
    }
}