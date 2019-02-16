using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication5.Controllers;

namespace WebApplication5
{
    public class Startup
    {
        private static Guid _userId = Guid.Empty;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
                var controller = new ValuesController();
                var login = context.Request.Cookies["login"];
                if (!context.Request.Cookies.ContainsKey("login")
                    && !context.Request.Path.StartsWithSegments(new PathString("/authPage")))
                {
                    context.Response.Redirect("/authPage");
                }

                if (context.Request.Cookies.ContainsKey("login") 
                    && controller.ContainUser(login)
                    && context.Request.Path.StartsWithSegments(new PathString("/authPage")))
                {
                    context.Response.Redirect("/Home");
                }

                return next.Invoke();
            });

            app.Map("/Check", builder => builder.Run(async context =>
            {
                var form = context.Request.Form;
                var login = form["login"];
                var password = form["password"];
                var controller = new ValuesController();
                controller.AddUser(login, password);
                _userId = controller.GetUser(login).Id;
                context.Response.Cookies.Append("login", login);
                context.Response.Redirect("/Home");
            }));

            app.Map("/Add", builder => builder.Run(async context =>
            {
                var form = context.Request.Form;
                var text = form["text"];
                var title = form["title"];
                var file = context.Request.Form.Files["file"];
                var filePath = $"files/{file.FileName}";
                var login = context.Request.Cookies["login"];

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                    file.CopyTo(fileStream);

                using (var controller = new ValuesController())
                    controller.AddNote(title, text, filePath, controller.GetUser(login).Id);

                await LoadPageInResponse(context, "confirmation");
            }));

            app.Map("/Home", builder => builder.Run(async context =>
            {
                var htmlPage = File.ReadAllText("Views/Home.html");
                htmlPage = htmlPage.Replace("@List", GetFiles());
                using (var streamWriter = new StreamWriter(context.Response.Body))
                {
                    await streamWriter.WriteAsync(htmlPage);
                }
            }));

            app.Map("/authPage", builder => builder.Run(async context =>
            {
                var htmlPage = File.ReadAllText("Views/authPage.html");
                using (var streamWriter = new StreamWriter(context.Response.Body))
                {
                    await streamWriter.WriteAsync(htmlPage);
                }
            }));

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


            app.Map("/Notes", builder => builder.Run(async context => { await LoadPageInResponse(context, "notes"); }));

            #endregion

            app.Run(async context =>
            {
                context.Response.StatusCode = 308;
                context.Response.Headers["Location"] = "/Home";
                await Task.CompletedTask;
            });
        }

        private static Task LoadPageInResponse(HttpContext context, string pageName)
        {
            var page = File.ReadAllText($"Views/{pageName}.html");
            using (var streamWriter = new StreamWriter(context.Response.Body))
                return streamWriter.WriteAsync(page);
        }

        private static string GetFiles()
        {
            using (var controller = new ValuesController())
            {
                var files = controller.GetNotes(_userId);

                var sb = new StringBuilder();
                sb.Append(@"<ul style=""padding-left: 50px; padding-top: 15px;"">");
                foreach (var file in files)
                {
                    sb.Append($@"<li><a href=""texts?name={file}"">{file}</a></li>");
                }

                sb.Append("</ul>");
                return sb.ToString();
            }
        }
    }
}