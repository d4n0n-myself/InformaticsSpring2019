using System.Collections.Generic;
using System.Linq;

namespace WebApplication5
{
    public class Repository
    {
        public Repository(DataBase context)
        {
            this.context = context;
        }

        public Repository()
        {
            context = new DataBase();
        }

        private DataBase context;

        public void AddNote(string Header, string Body) //string FileLink)
        {
            var context = new DataBase();
            context.Notes.Add(new Note(Header, Body));
            context.SaveChanges();
        }

        public Note GetNoteByHeader(string Header) => context.Notes.First(n => n.Header.Equals(Header));
        public List<string> GetHeaders() => context.Notes.Select(n => n.Header).ToList();
        public bool ContainNote(string Header) => context.Notes.Any(n => n.Header.Equals(Header));
    }
}