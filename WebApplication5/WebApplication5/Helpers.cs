using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication5.Controllers;

namespace WebApplication5
{
	/// <summary>
	/// This class provides intermediate support.
	/// </summary>
	internal class Helpers
	{
		internal static Task LoadPageInResponse(HttpContext context, string pageName)
		{
			var page = File.ReadAllText($"Views/{pageName}.html");
			using (var streamWriter = new StreamWriter(context.Response.Body))
				return streamWriter.WriteAsync(page);
		}

		internal static string GetFiles(HttpContext context)
		{
			using (var controller = new NotesController())
			{
				var files = controller.Get(context);

				var sb = new StringBuilder();
				sb.Append(@"<ul style=""padding-left: 50px; padding-top: 15px;"">");
				foreach (var file in files)
					sb.Append($@"<li><a href=""texts?name={file}"">{file}</a></li>");

				sb.Append("</ul>");
				return sb.ToString();
			}
		}
	}
}