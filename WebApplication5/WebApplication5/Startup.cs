using System.Data;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication5.Controllers;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            #region Mappers

            app.Use((context, next) =>
            {
                if (!context.Request.Cookies.ContainsKey("login") &&
                    !context.Request.Path.StartsWithSegments(new PathString("/Home/Authentificate")))
                    context.Response.Redirect("/Home/Authentificate");
                if (context.Request.Cookies.ContainsKey("login") &&
                    context.Request.Path.StartsWithSegments(new PathString("/Home/Authentificate")))
                    context.Response.Redirect("/Home");
                return next.Invoke();
            });

            app.MapWhen(context => context.Request.Path.StartsWithSegments(new PathString("/texts")),
                builder => builder.Run(async httpContext =>
                {
                    var title = httpContext.Request.Query["name"];
                    using (var controller = new ValuesController())
                    {
                        if (!controller.ContainNote(title))
                            throw new DataException("No such note");

                        var note = controller.GetNote(title);

                        await httpContext.Response.WriteAsync($@"<a href=""showfile/{note.FileLink}"">File</a>");
                        await httpContext.Response.WriteAsync("\r\n");
                        await httpContext.Response.WriteAsync(note.Body);
                    }
                }));

            app.MapWhen(context => context.Request.Path.StartsWithSegments(new PathString("/showfile")),
                builder => builder.Run(async context =>
                {
                    var filePath = $"files/{context.Request.Path.ToUriComponent().Split('/').Last()}";

                    if (File.Exists(filePath))
                        using (var file = File.Open(filePath, FileMode.Open))
                            file.CopyTo(context.Response.Body);
                }));

            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    "Default", // Route name
                    "", // URL with parameters
                    new {controller = "Home", action = "Index"} // Parameter defaults
                );
            });
        }
    }
}