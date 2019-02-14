using System;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace WebApplication5
{
    public class Note
    {
       public Guid Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string FileLink { get; set; }

        public Note(string header, string body)//, string fileLink)
        {
            Id = Guid.NewGuid();
            Header = header;
            Body = body;
            //FileLink = fileLink;
        }

        public Note()
        {
            var x = 1;
        }
    }
}