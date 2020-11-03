using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interesse.DbLayer.Configuration;
using Interesse.DbLayer.Context;
using Interesse.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Interesse.ServiceLayer.Services.Interfaces;
using Interesse.ServiceLayer.Services;
using Interesse.App.Extensions;

namespace Interesse.App
{
    public class Startup
    {
        private string _contentRootPath = "";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _contentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conn = Configuration.GetConnectionString("DefaultConnection");
            if (conn.Contains("%CONTENTROOTPATH%"))
            {
                conn = conn.Replace("%CONTENTROOTPATH%", _contentRootPath);
            }

            DbConfig.ConfigureDb(services, conn);

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>();
            services.AddHttpContextAccessor();

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/login");
                options.Conventions.AddPageRoute("/Admin/Index", "/control");
                options.Conventions.AddPageRoute("/ContentPage", "{*url}");
                options.Conventions.AddPageRoute("/PageCategory", "/category/{url}");
                options.Conventions.AuthorizeFolder("/Admin");
            });

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ITeacherService, TeacherService>();
            services.AddTransient<IVacancyService, VacancyService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IPageCategoryService, PageCategoryService>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<ITemplateImageService, TemplateImageService>();
            services.AddTransient<IFileService, FileService>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            ApplicationDbInitializer.SeedUsers(userManager);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
