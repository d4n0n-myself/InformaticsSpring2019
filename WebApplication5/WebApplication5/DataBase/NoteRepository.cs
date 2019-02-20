using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication5.Database.Entites;

namespace WebApplication5.Database
{
	public class NoteRepository
	{
		public NoteRepository(DataBase context)
		{
			_context = context;
		}

		public NoteRepository()
		{
			_context = new DataBase();
		}

		public void AddNote(string header, string body, string fileLink, string userLogin)
		{
			_context.Notes.Add(new Note(header, body, fileLink, userLogin));
			_context.SaveChanges();
		}

		public bool ContainNote(string header) => _context.Notes.Any(n => n.Header.Equals(header));

		public Note GetNoteByHeader(string header) => _context.Notes.First(n => n.Header.Equals(header));

		public List<string> GetHeaders(string login) =>
			_context.Notes 
				.Where(n => n.UserLogin.Equals(login)) 
				.Select(n => n.Header)
				.ToList();

		private readonly DataBase _context;
	}
}