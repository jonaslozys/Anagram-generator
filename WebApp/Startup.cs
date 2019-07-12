using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Configuration;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.configurations;
using AnagramGenerator.BusinessLogic;
using AnagramGenerator.EF.DatabaseFirst.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace WebApp
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
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<Connection>(Configuration);
            services.AddDbContext<AnagramsContext>(options => options.UseSqlServer(Configuration.GetSection("ConnectionString").Value));
            services.Configure<AnagramConfiguration>(Configuration.GetSection("AnagramConfiguration"));
            services.AddSingleton(Configuration);
            services.AddSingleton<IWordsRepository, WordsRepository>();
            services.AddScoped<IEfWordsRepository, EfWordsRepository>();
            services.AddSingleton<ICacheRepository, CacheRepository>();
            services.AddSingleton<IUsersRepository, UsersRepository>();
            services.AddScoped<IAnagramsService, AnagramsService>();
            services.AddScoped<IAnagramSolver, AnagramSolver>();
            services.Configure<Dictionary>(Configuration.GetSection("Dictionary"));
            services.Configure<AnagramSettings>(Configuration.GetSection("AnagramSettings"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
