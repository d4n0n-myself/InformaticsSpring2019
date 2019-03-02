using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerExample.Controllers
{
	/// <summary>
	/// Is this a controller ?
	/// </summary>
	[Route("[controller]/[action]")]
	public class SampleDataController : Controller
	{
		private static string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};


		/// <summary>
		/// Is this loss ?
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IEnumerable<WeatherForecast> WeatherForecasts()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			});
		}

		/// <summary>
		/// Hello, i am still here!
		/// </summary>
		[HttpPost]
		public void OmgItsASwagger()
		{
			Console.Write("Hello its me!");
		}

		public class WeatherForecast
		{
			public string DateFormatted { get; set; }
			public int TemperatureC { get; set; }
			public string Summary { get; set; }

			public int TemperatureF
			{
				get { return 32 + (int) (TemperatureC / 0.5556); }
			}
		}
	}
}