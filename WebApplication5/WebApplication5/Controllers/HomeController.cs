using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
	/// <summary>
	///	This controller provides HTML views.
	/// </summary>
	[ResponseCache(Duration = 60)]
	public class HomeController : Controller
	{
		public void Add()
		{
			using(var notesController = new NotesController())
				notesController.Add();

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
			{
				Response.Cookies.Append("login", "ok");
				Response.Cookies.Append("userId", login);
			}

			Response.Redirect("/Home");
		}

		public void Index()
		{
			var htmlPage = System.IO.File.ReadAllText("Views/Home.html");
			htmlPage = htmlPage.Replace("@List", Helpers.GetFiles());
			using (var streamWriter = new StreamWriter(Response.Body))
				streamWriter.WriteAsync(htmlPage);
		}

		public void Notes()
		{
			Helpers.LoadPageInResponse(HttpContext, "notes");
		}
	}
}