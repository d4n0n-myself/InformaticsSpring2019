using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Database;
using WebApplication5.Database.Entites;

namespace WebApplication5.Controllers
{
	/// <summary>
	///     MVC используется как реализация IDisposable
	/// </summary>
	public class ValuesController : Controller
	{
		public void AddNote(string title, string text, string fileLink, Guid userId)
		{
			Repo.AddNote(title, text, fileLink, userId);
		}

		//[ResponseCache(Duration = 30)]
		public List<string> GetNotes()
		{
			///var cookie = Guid.Parse(Request.Cookies["userId"]);
			return Repo.GetHeaders(Guid.NewGuid());
		}

		public bool ContainNote(string header) => Repo.ContainNote(header);

		public Note GetNote(string header) => Repo.GetNoteByHeader(header);

		private static readonly NoteRepository Repo = new NoteRepository();
	}
}