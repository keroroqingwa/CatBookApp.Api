using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CatBookApp;
using CatBookApp.Configuration;
using CatBookApp.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;

namespace WechatMiniProgram.Api
{
    [DependsOn(
        typeof(CatBookAppApplicationModule),
        typeof(CatBookAppEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreModule))]
    public class ApiModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public ApiModule(IWebHostEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(CatBookAppConsts.ConnectionStringName);

            //Configuration.Navigation.Providers.Add<CatNavigationProvider>();

            //创建动态Web Api
            //Configuration.Modules.AbpAspNetCore()
            //    .CreateControllersForAppServices(
            //        typeof(CatBookAppApplicationModule).GetAssembly()
            //    );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ApiModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(ApiModule).Assembly);
        }
    }
}