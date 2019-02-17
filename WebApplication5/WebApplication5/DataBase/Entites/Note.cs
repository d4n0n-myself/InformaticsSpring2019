using System;

namespace WebApplication5.Database.Entites
{
	public class Note
	{
		public Note(string header, string body, string fileLink, Guid userId)
		{
			Id = Guid.NewGuid();
			UserId = userId;
			Header = header;
			Body = body;
			FileLink = fileLink;
		}

		public Note()
		{
		}

		public Guid UserId { get; set; }
		public Guid Id { get; set; }
		public string Header { get; set; }
		public string Body { get; set; }
		public string FileLink { get; set; }
	}
}