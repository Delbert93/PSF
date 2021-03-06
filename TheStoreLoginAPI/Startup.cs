using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheStoreLoginAPI.Data;
using Serilog;


namespace TheStoreLoginAPI
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(convertUrlConnectionString(Configuration["DATABASE_URL"])));
            services.AddScoped<IRepository, EfCoreRepository>();
            services.AddRazorPages();

            services.AddFeatureManagement();

            services.AddControllers();

            services.AddSession();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                            .WithOrigins("http://localhost:62788", "http://localhost:5000", "https://delbert93.github.io", "http://localhost:5005")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static string convertUrlConnectionString(string url)
        {
            if (url is null)
                throw new ArgumentNullException("It appears you're missing the DATABASE_URL configuration value...");
            if (!url.Contains("//"))
                return url;
            var uri = new Uri(url);
            var host = uri.Host;
            var port = uri.Port;
            var database = uri.Segments.Last();
            var parts = uri.AbsoluteUri.Split(':', '/', '@');
            var user = parts[3];
            var password = parts[4];

            return $"host={host}; port={port}; database={database}; username={user}; password={password}; SSL Mode=Prefer; Trust Server Certificate=true";
        }
    }
}
