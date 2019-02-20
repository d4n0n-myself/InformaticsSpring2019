using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Database;
using WebApplication5.Database.Entites;

namespace WebApplication5.Controllers
{
	/// <summary>
	///	This class collects information about notes.
	/// </summary>
	public class NotesController : IDisposable
	{
		public void Add(HttpContext context)
		{
			var form = context.Request.Form;
			var text = form["text"];
			var title = form["title"];
			var file = form.Files["file"];
			string userId;
			try
			{
				userId = context.Request.Cookies["userId"];
			}
			catch (Exception e)
			{
				context.Response.Redirect("/Home/Authentificate");
				return;
			}
			

			if (file != null)
			{
				var filePath = $"files/{file.FileName}";

				using (var fileStream = new FileStream(filePath, FileMode.Create))
					file.CopyTo(fileStream);

					Repo.AddNote(title, text, filePath, userId);
			}
			else
				Repo.AddNote(title, text, null, userId);		
		}

		[ResponseCache(Duration = 30)]
		public List<string> Get(HttpContext context)
		{
			//TODO
//			Guid userId;
//			try
//			{
//				userId = Guid.Parse(context.Request.Cookies["userId"]);
//				
//			}
//			catch 
//			{
//				//context.Response.Redirect("/Home/Authentificate");
//				return new List<string>();
//			}

			try
			{
				//TODO
				return Repo.GetHeaders(null);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public bool Contains(string header) => Repo.ContainNote(header);

		public Note Get(string header) => Repo.GetNoteByHeader(header);

		private static readonly NoteRepository Repo = new NoteRepository();
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}