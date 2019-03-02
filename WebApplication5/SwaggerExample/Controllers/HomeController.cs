using System;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerExample.Controllers
{
	/// <summary>
	/// This is just a controller.
	/// </summary>
	[Route("[controller]/[action]")]
	public class HomeController : Controller
	{
		/// <summary>
		/// Add this is just a method.
		/// </summary>
		[HttpGet]
		public void Method()
		{
			Console.Write("I am a method!");
		}
	}
}