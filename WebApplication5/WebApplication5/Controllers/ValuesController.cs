using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
	/// <summary>
	/// MVC используется как реализация IDisposable
	/// </summary>
	public class ValuesController : Controller 
	{
		static Repository repo = new Repository();
		public void AddNote(string title, string text) => repo.AddNote(title, text);

		public List<string> GetNotes() => repo.GetHeaders();

		public bool ContainNote(string Header) => repo.ContainNote(Header);

		public Note GetNote(string Header) => repo.GetNoteByHeader(Header);
	}
}