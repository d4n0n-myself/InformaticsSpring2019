using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Database;

namespace WebApplication5.Controllers
{
	/// <summary>
	///     MVC используется как реализация IDisposable
	/// </summary>
	public class ValuesController : Controller
	{
		private static readonly Repository repo = new Repository();

		public void AddNote(string title, string text, string fileLink)
		{
			repo.AddNote(title, text, fileLink);
		}

		public List<string> GetNotes()
		{
			return repo.GetHeaders();
		}

		public bool ContainNote(string Header)
		{
			return repo.ContainNote(Header);
		}

		public Note GetNote(string Header)
		{
			return repo.GetNoteByHeader(Header);
		}
	}
}