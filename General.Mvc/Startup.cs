using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Core;
using General.Core.data;
using General.Core.Extensions;
using General.Core.Librarys;
using General.Entities;
using General.Framework.Infrastructure;
using General.Framework.Security.Admin;
using General.Services.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace General.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            services.AddDbContext<GeneralDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")),ServiceLifetime.Scoped);

            services.AddAuthentication("General").AddCookie(o =>
                o.LoginPath = "/Admin/Login/Index"
            );

            //services.AddScoped<IUserService, UserService>();
            //程序集注入
            services.AddAssembly("General.Services", ServiceLifetime.Scoped);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IWorkContext, WorkContext>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //services.BuildServiceProvider().GetService<IUserService>();
            //将services.BuildServiceProvider()保存在静态上下文引擎对象中，供外部调用
            EngineContext.Initialize(new GeneralEngine(services.BuildServiceProvider()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Login}/{action=Index}/{id?}"
                );
            });
        }
    }
}
