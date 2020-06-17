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

namespace Backstage.Api
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

            //���jwt��֤
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = "jwttest",//Audience
                    ValidIssuer = "jwttest",//Issuer���������ǰ��ǩ��jwt������һ��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]))//�õ�SecurityKey
                };
            });

            //�޸�Razor��ͼ��ʱˢ��
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

            services.Configure<MvcOptions>(mvcOptions =>
            {
                //�Զ����쳣����
                mvcOptions.Filters.AddService(typeof(Utils.CatExceptionFilter));
                ////�Զ���api�ӿ����ݷ���
                //mvcOptions.Filters.AddService(typeof(Utils.CatResultFilter));
            });

            // swagger
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v3", new OpenApiInfo
                {
                    Title = "���������项С�����к�̨���api",
                    Version = "v3",
                    Description = "���ǡ��������项΢��С������к�̨�ӿڳ��������κ����ʿɼ�QQȺ��875607244",
                    Contact = new OpenApiContact() { Name = "�鿴 ���ܵ� GitHub", Email = "514158268@qq.com", Url = new Uri("https://github.com/keroroqingwa/CatBookApp.Api") }
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var xmlPath = Path.Combine(_env.ContentRootPath, "Backstage.Api.xml");
                c.IncludeXmlComments(xmlPath, true);

                #region Authorization
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });

                var oar = new OpenApiSecurityRequirement();
                oar.Add(
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    }, new List<string>());
                c.AddSecurityRequirement(oar);
                #endregion
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

            app.UseAuthentication(); //��Ȩ
            app.UseAuthorization();  //�����֤

            //swagger
            app.UseSwagger();
            //app.UseSwaggerUI(); //URL: /swagger/ui
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v3/swagger.json", "v3");
                c.RoutePrefix = string.Empty; //���ӿ��ĵ�����Ϊ��ҳ
                c.DocumentTitle = "�к�̨���api�ĵ� -���������项С����"; //�ĵ�����
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
