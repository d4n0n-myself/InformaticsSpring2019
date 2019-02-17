using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
	[ResponseCache(Duration = 90)]
	public class HomeController : Controller
	{
		public void Add()
		{
			var form = Request.Form;
			var text = form["text"];
			var title = form["title"];
			var file = form.Files["file"];
			//var userGuid = Guid.Parse(Request.Cookies["userId"]);

			if (file != null)
			{
				var filePath = $"files/{file.FileName}";

				using (var fileStream = new FileStream(filePath, FileMode.Create))
					file.CopyTo(fileStream);

				using (var controller = new ValuesController())
					controller.AddNote(title, text, filePath, Guid.NewGuid());//userGuid);
			}
			else
				using (var controller = new ValuesController())
					controller.AddNote(title, text, null, Guid.NewGuid()); //userGuid);

			Helpers.LoadPageInResponse(HttpContext, "confirmation");
		}

		public void Authentificate()
		{
			Helpers.LoadPageInResponse(HttpContext, "authPage");
		}

		public void Check()
		{
			var form = Request.Form;
			var login = form["login"];
			var password = form["password"];
			if (login == "admin" && password == "admin")
				Response.Cookies.Append("login", "ok");

			Response.Redirect("/Home");
		}

		public void Index()
		{
			var htmlPage = System.IO.File.ReadAllText("Views/Home.html");
			htmlPage = htmlPage.Replace("@List", Helpers.GetFiles());
			using (var streamWriter = new StreamWriter(Response.Body))
			{
				streamWriter.WriteAsync(htmlPage);
			}
		}

		public void Notes()
		{
			Helpers.LoadPageInResponse(HttpContext, "notes");
		}
	}
}