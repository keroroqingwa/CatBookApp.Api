using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Session;
using Backstage.Api.Utils;
using CatBookApp;
using CatBookApp.Configuration;
using CatBookApp.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;

namespace Backstage.Api
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

            //替换IAbpSession的实现类
            Configuration.ReplaceService(typeof(IAbpSession), () =>
            {
                IocManager.Register<IAbpSession, MyAbpSession>();
            });
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