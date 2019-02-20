using System;

namespace WebApplication5.Database.Entites
{
	public class Note
	{
		public Note(string header, string body, string fileLink, string login)
		{
			Id = Guid.NewGuid();
			UserLogin = login;
			Header = header;
			Body = body;
			FileLink = fileLink;
		}

		public Note()
		{
		}

		public string UserLogin { get; set; }
		public Guid Id { get; set; }
		public string Header { get; set; }
		public string Body { get; set; }
		public string FileLink { get; set; }
	}
}