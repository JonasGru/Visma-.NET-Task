using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace VismaLibraryTask
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }
        public bool? Taken { get; set; }
        public string? TakenBy { get; set; }
        public DateTime? TakenUntil { get; set; }


        public Book(string bookName, string auth, string cat, string lang, DateTime bookTime, string isnb)
        {
            Name = bookName;
            Author = auth;
            Category = cat;
            Language = lang;
            PublicationDate = bookTime;
            ISBN = isnb;
            Taken = false;
        }
    }
}
