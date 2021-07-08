using System;
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
            Book book = JsonSerializer.Deserialize<Book>(jsonString);
            Console.WriteLine($"Name: {book.name}");
            Console.WriteLine($"ISBN: {book.ISBN}");
            Console.ReadKey();
        }
    }
}
