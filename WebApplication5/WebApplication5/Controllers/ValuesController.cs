using System;
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

		public void AddNote(string title, string text, string fileLink, Guid userId)
		{
			repo.AddNote(title, text, fileLink, userId);
		}
    
		public void AddUser(string login, string password)
		{
			repo.AddUser(login, password);
		}
    
    [ResponseCache(Duration = 30)]
		public List<string> GetNotes(Guid UserId)
		{
			return repo.GetHeaders(UserId);
		}

		public bool ContainNote(string Header)
		{
			return repo.ContainNote(Header);
		}

		public bool ContainUser(string Login)
		{
			return repo.ContainUser(Login);
		}
		
		public Note GetNote(string Header)
		{
			return repo.GetNoteByHeader(Header);
		}

		public User GetUser(string login)
		{
			return repo.GetUserByLogin(login);
		}
	}
}