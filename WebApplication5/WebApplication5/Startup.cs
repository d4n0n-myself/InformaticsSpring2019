using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebApplication5.Routers;

namespace WebApplication5
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
            services.AddRouting();
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("1.0.0", new OpenApiInfo()
                {
                    Title = "d4n0n's API", 
                    Version = "1.0.0",
                    Contact = new OpenApiContact()
                    {
                        Email = "danon.sibaev@yandex.ru",
                        Name = "d4n0n_myself"
                    },
                    Description = "Informatics Spring 2019 project. Uses ASP.NET Core MVC pattern."
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
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
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMiddleware<AuthentificationMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "d4n0n's API 1.0.0");
                options.RoutePrefix = String.Empty;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("v1", "{controller=Home}/{action=Index}");
                routes.MapRoute("v2", "", new {controller="Home", action="Index"});
                routes.Routes.Add(new FileRouter());
                routes.Routes.Add(new TextRouter());
            });
        }
    }
}