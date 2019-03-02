using System;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerExample.Controllers
{
	/// <inheritdoc />
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

		/// <summary>
		/// Im a method with parameter!
		/// </summary>
		/// <param name="id">ID Parameter</param>
		[HttpGet]
		public void MethodWtihParameter(int id)
		{
			Console.Write("I have a parameter!");
		}
	}
}