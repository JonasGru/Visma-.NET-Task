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
        static readonly string fileName = "../../../bookFile.json";
        static List<Book> booksInLibrary;
        static void Main()
        {
            Console.WriteLine("Welcome to the library!");

           // Program p = new();



            //string jsonString = File.ReadAllText(fileName);
            //Console.WriteLine(jsonString);
            ReadFromFile();
            MainMenu();


            //Book book = JsonSerializer.Deserialize<Book>(jsonString);
            //List<Book> booksInLibrary = JsonSerializer.Deserialize<List<Book>>(jsonString);




            //Book testBook = new Book("Test", "author", "testing", "English", DateTime.Parse("2000-01-01"), "0-000-000-000-001");

            //string jsonString = JsonSerializer.Serialize(testBook);

            //Console.WriteLine(jsonString);

            //SaveLibrary();
            Console.ReadKey();
        }

        public static void MainMenu()
        {
            Console.WriteLine($"{"1 - Library contents", 15} | {"2 - Insert Book",15} | {"1 - Library contents",15} | {"9 - Save Library",15} | {"0 - Exit Library",15} |");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    PrintLibrary();
                    break;
                case "2":
                    InsertBook();
                    break;
                case "3":
                    PrintLibrary();
                    break;
                case "9":
                    SaveLibrary();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("input didn't match any of the options, please try again");
                    break;
            }
            Console.WriteLine();
            MainMenu();
        }

        static void ReadFromFile()
        {
            string jsonString = File.ReadAllText(fileName);
            booksInLibrary = JsonConvert.DeserializeObject<List<Book>>(jsonString);
        }

        public static void PrintLibrary()
        {
            Console.WriteLine($"{"Name", 15} | {"Author", 15} | {"Category",15} | {"Language",10} | {"P_Date",10} | {"ISBN",17} ");
            foreach (Book b in booksInLibrary)
            {
                Console.WriteLine($"{b.Name, 15} | {b.Author, 15} | {b.Category, 15} | {b.Language,10} | {b.PublicationDate.Year,10} | {b.ISBN,17}");
            }
        }

        public static void InsertBook()
        {
            Console.WriteLine("write a book to insert:");
            Console.WriteLine($"{"Name (string)",15} | {"Author (string)",15} | {"Category (string)",15} | {"Language (string)",10} | {"P_Date (DateTime)",10} | {"ISBN (string)",17} ");

            Console.WriteLine("Name (string):");
            string name = Console.ReadLine();

            Console.WriteLine("Author (string):");
            string author = Console.ReadLine();

            Console.WriteLine("Category (string):");
            string category = Console.ReadLine();

            Console.WriteLine("Language (string):");
            string language = Console.ReadLine();

            Console.WriteLine("Publication Date (YYYY MM DD):");
            DateTime p_date = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("ISBN (string):");
            string isbn = Console.ReadLine();

            try
            {
                Book instertBook = new(name, author, category, language, p_date, isbn);
                AddBookToLibrary(instertBook);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void AddBookToLibrary(Book bookToInsert)
        {
            booksInLibrary.Add(bookToInsert);
        }

        public static void TakeBook()
        {

        }

        public static void ReturnBook()
        {

        }
        public static void DeleteBook()
        {

        }

        public static void SaveLibrary()
        {
            string jsonString = JsonConvert.SerializeObject(booksInLibrary);
            //Console.WriteLine(jsonString);
            File.WriteAllText(fileName, jsonString);
            //File.ReadAllText(fileName);
            //booksInLibrary = JsonConvert.DeserializeObject<List<Book>>(jsonString);
        }
    }
}
