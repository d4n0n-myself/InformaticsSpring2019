using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WebApplication5.Controllers;

namespace WebApplication5.Routers
{
	public class TextRouter : IRouter
	{
		public Task RouteAsync(RouteContext context)
		{
			var httpContext = context.HttpContext;
			if (!context.HttpContext.Request.Path.Value.Contains("texts")) return Task.CompletedTask;
			var title = httpContext.Request.Query["name"];
			using (var controller = new NotesController())
			{
				if (!controller.Contains(title))
					throw new DataException("No such note");

				var note = controller.Get(title);

				if (note.FileLink != null)
					httpContext.Response.WriteAsync($@"<a href=""showfile/{note.FileLink}"">File</a>");
				httpContext.Response.WriteAsync("<br>");
				httpContext.Response.WriteAsync(note.Body);
			}
			return Task.CompletedTask;
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			throw new System.NotImplementedException();
		}
	}
}