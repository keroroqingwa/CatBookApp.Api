using Abp.Modules;
using Abp.Reflection.Extensions;
using CatBookApp.Localization;

namespace CatBookApp
{
    public class CatBookAppCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            CatBookAppLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CatBookAppCoreModule).GetAssembly());
        }
    }
}