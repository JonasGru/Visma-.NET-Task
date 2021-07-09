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
            Console.WriteLine($"{"1 - Library contents", -15} | {"2 - Insert Book",-15} | {"3 - TakeBook",-15} | {"4 - ReturnBook",-15} | {"9 - Save Library",-15} | {"0 - Exit Library",-15} |");
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
                    TakeBook();
                    break;
                case "4":
                    ReturnBook();
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
            Console.WriteLine($"{"NR", 4} | {"Name", -15} | {"Author", -15} | {"Category",-15} | {"Language",-10} | {"P_Date",-10} |" +
                $" {"ISBN",-17} | {"Taken?",-6} | {"TakenBy",-10} | {"TakenUntil",-17}");
            int nr = 0; //faster than booksInLibrary.IndexOf(b)?
            foreach (Book b in booksInLibrary)
            {
                Console.WriteLine($"{nr++,4} | {b.Name, -15} | {b.Author, -15} | {b.Category, -15} | {b.Language,-10} | {b.PublicationDate.Year,-10}" +
                    $" | {b.ISBN,-17} | {b.Taken, -6} | {b.TakenBy,-10} | {b.TakenUntil,-17}");
            }
        }

        public static void InsertBook()
        {
            Console.WriteLine("write a book to insert:");
            //Console.WriteLine($"{"Name (string)",15} | {"Author (string)",15} | {"Category (string)",15} | {"Language (string)",10} | {"P_Date (DateTime)",10} | {"ISBN (string)",17} ");
            try
            {
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

                Book instertBook = new(name, author, category, language, p_date, isbn);
                AddBookToLibrary(instertBook);
            }
            catch (Exception)
            {
                Console.WriteLine("Field inserted with a wrong value, try again");
                InsertBook();
                throw;
            }

            
        }

        public static void AddBookToLibrary(Book bookToInsert)
        {
            booksInLibrary.Add(bookToInsert);
        }

        public static void TakeBook()
        {
            PrintLibrary();
            Console.WriteLine("select a book to take (write it's NR to console):");
            int nr;
            try
            {
                nr = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("please write an integer (number)");
                //TakeBook();
                throw;
            }
            Console.WriteLine("who will be taking this book?:");
            string takenBy = Console.ReadLine();

            Console.WriteLine("how long will you be borrowing this book? (MM DD, max 2 months):");
            string borrowingLengh = Console.ReadLine();
            DateTime date;// = DateTime.Parse(borrowingLengh);
            try
            {
                date = DateTime.Parse(borrowingLengh);
            }
            catch (Exception)
            {
                Console.WriteLine("input is in wrong format");
                throw;
            }
            TakeBookFromLibrary(nr, takenBy, date);
        }

        public static void TakeBookFromLibrary(int nr, string personBorrowing, DateTime date)
        {
            List<Book> results = booksInLibrary.FindAll(delegate (Book b) { return b.TakenBy == personBorrowing; });
            //Console.WriteLine("person borrowed: " + results.Count);
            if(results.Count > 2)
            {
                Console.WriteLine($"person already borrowed {results.Count} books, we only allow borrowing up to 3 books!");
                return;
            }
            if (date.CompareTo(DateTime.Parse("02 01")) < 1)
            {
                try
                {
                    booksInLibrary[nr].Taken = true;
                    booksInLibrary[nr].TakenBy = personBorrowing;
                    booksInLibrary[nr].TakenUntil = DateTime.Now.AddMonths(date.Month).AddDays(date.Day);
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("book not found");
                    return;
                    throw;
                }
                catch (Exception)
                {

                    throw;
                }
               
            }
            else
            {
                Console.WriteLine("borrowing term is too long, we only borrow books for 2 months");
            }
        }

        public static void ReturnBook()
        {
            PrintLibrary();
            Console.WriteLine("select a book to return (write it's NR to console):");
            int nr;
            try
            {
                nr = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("please write an integer (number)");
                throw;
            }

            

            try
            {
                if (booksInLibrary[nr].Taken == false)
                {
                    Console.WriteLine("book wasn't taken by anyone");
                    return;
                }

                bool onTime = false;
                if (booksInLibrary[nr].TakenUntil > DateTime.Now)
                {
                    onTime = true;
                }

                booksInLibrary[nr].Taken = false;
                booksInLibrary[nr].TakenBy = null;
                booksInLibrary[nr].TakenUntil = null;

                Console.WriteLine(onTime? "book was returned" : "book was returned, but it was late! try harder next time!");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("book not found");
                return;
                throw;
            }
            catch (Exception)
            {

                throw;
            }
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
