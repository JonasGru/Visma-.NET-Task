using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VismaLibraryTask
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!");

            string fileName = "../../../bookFile.json";

            

            string jsonString = File.ReadAllText(fileName);
            Console.WriteLine(jsonString);
            //Book book = JsonSerializer.Deserialize<Book>(jsonString);
            //List<Book> booksInLibrary = JsonSerializer.Deserialize<List<Book>>(jsonString);
            List<Book> booksInLibrary =  JsonConvert.DeserializeObject<List<Book>>(jsonString);
            foreach (Book b in booksInLibrary)
            {
                Console.WriteLine($"Name: {b.Name}");
                Console.WriteLine($"ISBN: {b.ISBN}");
            }
            

            //Book testBook = new Book("Test", "author", "testing", "English", DateTime.Parse("2000-01-01"), "0-000-000-000-001");

            //string jsonString = JsonSerializer.Serialize(testBook);

            //Console.WriteLine(jsonString);

            Console.ReadKey();
        }
    }
}
