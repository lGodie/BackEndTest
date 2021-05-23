using BackEndTest.IoC;
using BackEndTest.Shared.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositories.Shared.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndTest
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
            services.AddDbContext<testContext>(cfg =>
            {
                cfg.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers();

            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
            // Init IoC Services
            DependencyContainer.AddDependency(services);

            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .WithExposedHeaders("X-Pagination"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin());


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
