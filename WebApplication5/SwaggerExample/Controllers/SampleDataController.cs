using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerExample.Controllers
{
	// чтобы не замарачиваться с названиями для Swagger, можно использовать названия из C#, прикрепив их с помощью шаблона
	[Route("[controller]/[action]")]
	public class SampleDataController : Controller
	{
		private static string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		
		[HttpGet]
		// или можно описать каждый метод с помощью шаблона : 
		// [HttpGet("[action]")]
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

		// или даже описать что-то более конкретное : 
		[HttpPost("myGet/[action]")]
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