using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WooliesXChallenge.Cache;
using WooliesXChallenge.Models;
using WooliesXChallenge.Services;
using WooliesXChallenge.Services.BackgroundTasks;
using WooliesXChallenge.Services.Interfaces;

namespace WooliesXChallenge
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Dependence Injection
            services.AddSingleton<IProductPopularityCache, ProductPopularityCache>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<ISortManager<Product>, ProductSortManager>();
            services.AddSingleton<IPopularityService, ProductPopularityService>();
            services.AddSingleton<ITrolleyService, TrolleyAPIService>();

            services.AddHostedService<RefreshCacheTask>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            ISortManager<Product> sortManager, IPopularityService popularityService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            ProductSortRegister.RegisterAll(sortManager, popularityService);
        }
    }
}
