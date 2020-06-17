using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using Castle.Facilities.Logging;
using CatBookApp.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WechatMiniProgram.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"Configs{Path.DirectorySeparatorChar}appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_configuration);

            ////添加jwt验证
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,//是否验证Issuer
            //            ValidateAudience = true,//是否验证Audience
            //            ValidateLifetime = true,//是否验证失效时间
            //            ValidateIssuerSigningKey = true,//是否验证SecurityKey
            //            ValidAudience = "jwttest",//Audience
            //            ValidIssuer = "jwttest",//Issuer，这两项和前面签发jwt的设置一致
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]))//拿到SecurityKey
            //        };
            //    });

            //修改Razor视图即时刷新
            services.AddMvc().AddRazorRuntimeCompilation();

            //Configure DbContext
            services.AddAbpDbContext<CatBookAppDbContext>(options =>
            {
                DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            });

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //自定义异常过滤
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.Filters.AddService(typeof(Utils.CatExceptionFilter));
            });

            // swagger
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "「喵喵看书」小程序后端api",
                    Version = "v3",
                    Description = "这是「喵喵看书」微信小程序的后端接口程序，如有任何疑问可加QQ群：875607244",
                    Contact = new OpenApiContact() { Name = "查看 青蛙的 GitHub", Email = "514158268@qq.com", Url = new Uri("https://github.com/keroroqingwa/CatBookApp.MiniProgram") }
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                var xmlPath = Path.Combine(_env.ContentRootPath, "WechatMiniProgram.Api.xml");
                c.IncludeXmlComments(xmlPath, true);
            });

            //Configure Abp and Dependency Injection
            return services.AddAbp<ApiModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig($"Configs{Path.DirectorySeparatorChar}log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); //Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();

            //app.UseAuthentication(); //授权
            //app.UseAuthorization();  //身份验证

            //swagger
            app.UseSwagger();
            //app.UseSwaggerUI(); //URL: /swagger/ui
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v3/swagger.json", "v3");
                c.RoutePrefix = string.Empty; //将接口文档设置为首页
                c.DocumentTitle = "小程序后端api文档 -「喵喵看书」"; //文档标题
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
