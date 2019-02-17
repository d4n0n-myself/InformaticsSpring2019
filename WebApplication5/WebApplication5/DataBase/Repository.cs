using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication5
{
	public class Repository
	{
		private readonly DataBase _context;

		public Repository(DataBase context)
		{
			_context = context;
		}

		public Repository()
		{
			_context = new DataBase();
		}

		public void AddNote(string header, string body, string fileLink, Guid userId)
		{
			_context.Notes.Add(new Note(header, body, fileLink, userId));
			_context.SaveChanges();
		}

		public void AddUser(string login, string password)
		{
			if(!_context.Users.Any(u => u.Login == login))
				_context.Users.Add(new User(login, password));
			_context.SaveChanges();
		}
		
		public Note GetNoteByHeader(string header)
		{
			return _context.Notes.First(n => n.Header.Equals(header));
		}

		public List<string> GetHeaders(Guid UserId)
		{
			return _context.Notes.Where(n => n.UserId == UserId).Select(n => n.Header).ToList();
		}

		public User GetUserByLogin(string login) => _context.Users.First(u => u.Login == login);

		public bool ContainNote(string header)
		{
			return _context.Notes.Any(n => n.Header.Equals(header));
		}

		public bool ContainUser(string login) => _context.Users.Any(u => u.Login.Equals(login));
	}
}