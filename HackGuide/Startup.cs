using HackGuide.PsnUtil;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackGuide
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
            services.AddControllersWithViews();
            //services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HackGuide Homebrew API", Version = "v1" });
            });
            services.AddSingleton(new HttpClient());
            services.AddSingleton(new PsnClient());
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HackGuide Homebrew API v1");
                c.RoutePrefix = "api";
            });

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        var cur = Directory.GetCurrentDirectory();
                        if (Directory.Exists($"{cur}/temp"))
                        {
                            var temps = Directory.GetDirectories($"{cur}/temp");
                            foreach (var item in temps)
                            {
                                var time = Directory.GetCreationTimeUtc(item);
                                if ((DateTime.UtcNow - time).TotalMinutes > 4)
                                {
                                    Directory.Delete($"{cur}/temp/{item}");
                                }
                            }
                        }
                        await Task.Delay(TimeSpan.FromMinutes(1));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        await Task.Delay(TimeSpan.FromMinutes(1));
                        continue;
                    }
                }
            });
        }
    }
}
