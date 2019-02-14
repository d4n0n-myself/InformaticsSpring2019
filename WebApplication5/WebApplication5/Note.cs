using System;

namespace WebApplication5
{
	public class Note
	{
		public Note(string header, string body, string fileLink)
		{
			Id = Guid.NewGuid();
			Header = header;
			Body = body;
			FileLink = fileLink;
		}

		public Note()
		{
		}

		public Guid Id { get; set; }
		public string Header { get; set; }
		public string Body { get; set; }
		public string FileLink { get; set; }
	}
}