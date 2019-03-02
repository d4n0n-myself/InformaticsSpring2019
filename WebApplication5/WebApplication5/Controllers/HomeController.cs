using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
    /// <summary>
    ///	This controller provides HTML views.
    /// </summary>
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public void Add()
        {
            using (var notesController = new NotesController())
                notesController.Add(Request.HttpContext);
            Helpers.LoadPageInResponse(HttpContext, "confirmation");
        }

        [HttpGet]
        public void Authentificate()
        {
            Helpers.LoadPageInResponse(HttpContext, "authPage");
        }

        [HttpGet]
        public void Check()
        {
            var controller = new UsersController();
            var form = Request.Form;
            var login = form["login"];
            var password = form["password"];
            if (!controller.Contains(login))
            {
                controller.Add(login, password);
                Response.Cookies.Append("login", "ok");
                Response.Cookies.Append("userId", login);
                Response.Redirect("/Home");
            }
            else if (controller.Contains(login) && controller.Check(login, password))
            {
                Response.Cookies.Append("login", "ok");
                Response.Cookies.Append("userId", login);
                Response.Redirect("/Home");
            }
        }

        /// <summary>
        /// Writes to response the main page of the site.
        /// </summary>
        [HttpGet]
        public void Index()
        {
            var htmlPage = System.IO.File.ReadAllText("Views/Home.html");
            htmlPage = htmlPage.Replace("@List", Helpers.GetFiles(Request.HttpContext));
            using (var streamWriter = new StreamWriter(Response.Body))
                streamWriter.WriteAsync(htmlPage);
        }

        [HttpGet]
        public void Notes()
        {
            Helpers.LoadPageInResponse(HttpContext, "notes");
        }
    }
}