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
        static string fileName = "../../../bookFile.json";
        static List<Book> booksInLibrary;
        static void Main()
        {
            Console.WriteLine("Hello World!");

           // Program p = new();



            //string jsonString = File.ReadAllText(fileName);
            //Console.WriteLine(jsonString);
            ReadFromFile();
            PrintLibrary();

            //Book book = JsonSerializer.Deserialize<Book>(jsonString);
            //List<Book> booksInLibrary = JsonSerializer.Deserialize<List<Book>>(jsonString);
            
            
            

            //Book testBook = new Book("Test", "author", "testing", "English", DateTime.Parse("2000-01-01"), "0-000-000-000-001");

            //string jsonString = JsonSerializer.Serialize(testBook);

            //Console.WriteLine(jsonString);

            Console.ReadKey();
        }

        static void ReadFromFile()
        {
            string jsonString = File.ReadAllText(fileName);
            booksInLibrary = JsonConvert.DeserializeObject<List<Book>>(jsonString);
        }

        public static void PrintLibrary()
        {
            Console.WriteLine($"{"Name", 15} | {"Author", 15} | {"Category",15} | {"Language",15} | {"PublicationDate",15} | {"ISBN",20} ");
            foreach (Book b in booksInLibrary)
            {
                Console.WriteLine($"{b.Name, 15} | {b.Author, 15} | {b.Category, 15} | {b.Language,15} | {b.PublicationDate.Year,15} | {b.ISBN,20}");
            }
        }
    }
}
