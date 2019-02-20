using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Database;
using WebApplication5.Database.Entites;

namespace WebApplication5.Controllers
{
	/// <summary>
	///	This class collects information about notes.
	/// </summary>
	public class NotesController : Controller
	{
		public void Add()
		{
			var form = Request.Form;
			var text = form["text"];
			var title = form["title"];
			var file = form.Files["file"];
			var userId = Guid.Parse(Request.Cookies["userId"]);

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
		public List<string> Get()
		{
			try
			{
				var userId = Guid.Parse(Request.Cookies["userId"]);
				return Repo.GetHeaders(userId);
			}
			catch 
			{
				return new List<string>();
			}
		}

		public bool Contains(string header) => Repo.ContainNote(header);

		public Note Get(string header) => Repo.GetNoteByHeader(header);

		private static readonly NoteRepository Repo = new NoteRepository();
	}
}