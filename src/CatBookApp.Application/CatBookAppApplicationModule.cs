using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace CatBookApp
{
    [DependsOn(
        typeof(CatBookAppCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CatBookAppApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CatBookAppApplicationModule).GetAssembly());
        }
    }
}