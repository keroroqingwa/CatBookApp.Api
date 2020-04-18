using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace CatBookApp.EntityFrameworkCore
{
    [DependsOn(
        typeof(CatBookAppCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class CatBookAppEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CatBookAppEntityFrameworkCoreModule).GetAssembly());
        }
    }
}