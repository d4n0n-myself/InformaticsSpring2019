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

		public void AddNote(string header, string body, string fileLink)
		{
			_context.Notes.Add(new Note(header, body, fileLink));
			_context.SaveChanges();
		}

		public Note GetNoteByHeader(string header)
		{
			return _context.Notes.First(n => n.Header.Equals(header));
		}

		public List<string> GetHeaders()
		{
			return _context.Notes.Select(n => n.Header).ToList();
		}

		public bool ContainNote(string header)
		{
			return _context.Notes.Any(n => n.Header.Equals(header));
		}
	}
}