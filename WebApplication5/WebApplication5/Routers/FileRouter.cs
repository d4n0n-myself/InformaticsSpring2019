using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;

namespace WebApplication5.Routers
{
	public class FileRouter : IRouter
	{
		public Task RouteAsync(RouteContext context)
		{
			if (context.HttpContext.Request.Path.Value.Contains("showfile"))
			{
				var filePath = $"files/{context.HttpContext.Request.Path.ToUriComponent().Split('/').Last()}";

				if (File.Exists(filePath))
					using (var file = File.Open(filePath, FileMode.Open))
						file.CopyTo(context.HttpContext.Response.Body);
			};
			return Task.CompletedTask;
		}

		public VirtualPathData GetVirtualPath(VirtualPathContext context)
		{
			throw new System.NotImplementedException();
		}
	}
}