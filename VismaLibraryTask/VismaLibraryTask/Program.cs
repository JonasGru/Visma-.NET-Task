using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VismaLibraryTask
{
    public class Program
    {
        public static readonly string fileName =  "../../../bookFile.json";
        public static List<Book> booksInLibrary;
        static void Main()
        {
            Console.WriteLine("Welcome to the library!");
            ReadFromFile();
            MainMenu();
            Console.ReadKey();
        }

        public static void MainMenu()
        {
            Console.WriteLine($"{"1 - Library contents", -15} | {"2 - Insert Book",-15} | {"3 - TakeBook",-15} | {"4 - ReturnBook",-15} | {"5 - FilterBooks",-15} " +
                $"| {"6 - DeleteBook",-15} | {"9 - Save Library",-15} | {"0 - Exit",-7} |");
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
                case "5":
                    FilterBooks();
                    break;
                case "6":
                    DeleteBook();
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

        public static void ReadFromFile()
        {
            string jsonString;
            try
            {
                jsonString = File.ReadAllText(fileName);
            }
            catch (FileNotFoundException)
            {
                jsonString = Properties.Resources.bookFile;  //for unit tests
                //throw;
            }
            //string jsonString = File.ReadAllText(fileName); //Properties.Resources.bookFile;//File.ReadAllText(fileName);
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

        public static void PrintLibrary(List<Book> listToPrint)
        {
            Console.WriteLine($"{"NR",4} | {"Name",-15} | {"Author",-15} | {"Category",-15} | {"Language",-10} | {"P_Date",-10} |" +
                $" {"ISBN",-17} | {"Taken?",-6} | {"TakenBy",-10} | {"TakenUntil",-17}");
            int nr = 0; //faster than booksInLibrary.IndexOf(b)?
            foreach (Book b in listToPrint)
            {
                Console.WriteLine($"{nr++,4} | {b.Name,-15} | {b.Author,-15} | {b.Category,-15} | {b.Language,-10} | {b.PublicationDate.Year,-10}" +
                    $" | {b.ISBN,-17} | {b.Taken,-6} | {b.TakenBy,-10} | {b.TakenUntil,-17}");
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
                return;
            }
            Console.WriteLine("who will be taking this book?:");
            string takenBy = Console.ReadLine();

            Console.WriteLine("how long will you be borrowing this book? (MM DD, max 2 months):");
            string borrowingLengh = Console.ReadLine();
            try
            {
                string[] dates = borrowingLengh.Split(' ');
                TakeBookFromLibrary(nr, takenBy, int.Parse(dates[0]), int.Parse(dates[1]));
            }
            catch (Exception)
            {
                Console.WriteLine("input is in wrong format");
                return;
            }
            
        }

        public static bool TakeBookFromLibrary(int nr, string personBorrowing, int dateMonths, int dateDays)
        {
            List<Book> results = booksInLibrary.FindAll(delegate (Book b) { return b.TakenBy == personBorrowing; });
            //Console.WriteLine("person borrowed: " + results.Count);
            if (results.Count > 2)
            {
                Console.WriteLine($"person already borrowed {results.Count} books, we only allow borrowing up to 3 books!");
                return false;
            }
            else if (booksInLibrary[nr].Taken == true)
            {
                Console.WriteLine($"sorry, the book is already taken, wait for it to be returned");
                return false;
            }
            else if (dateMonths*30 + dateDays <= 60)
            {
                try
                {
                    booksInLibrary[nr].Taken = true;
                    booksInLibrary[nr].TakenBy = personBorrowing;
                    booksInLibrary[nr].TakenUntil = DateTime.Now.AddMonths(dateMonths).AddDays(dateDays);
                    return true;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("book not found");
                    return false;
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
                return false;
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
                return;
            }

            ReturnBookToLibrary(nr);
        }

        public static bool ReturnBookToLibrary(int nr)
        {
            try
            {
                if (booksInLibrary[nr].Taken == false)
                {
                    Console.WriteLine("book wasn't taken by anyone");
                    return false;
                }

                bool onTime = false;
                if (booksInLibrary[nr].TakenUntil > DateTime.Now)
                {
                    onTime = true;
                }

                booksInLibrary[nr].Taken = false;
                booksInLibrary[nr].TakenBy = null;
                booksInLibrary[nr].TakenUntil = null;

                Console.WriteLine(onTime ? "book was returned" : "book was returned, but it was late! try harder next time!");
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("book not found");
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void FilterBooks()
        {
            Console.WriteLine($"{"1 - by author",-15} | {"2 - by category",-15} | {"3 - by language",-15} | {"4 - by ISBN",-15} | {"5 - by name",-15} " +
                $"| {"6 - by availability (isTaken?)",-15}");
            string input = Console.ReadLine();
            Console.WriteLine("type in filter criteria:");
            string filterBy = Console.ReadLine();


            FilterBooksInLibrary(input, filterBy);
        }

        public static void FilterBooksInLibrary(string input, string filterBy)
        {
            List<Book> results;
            switch (input)
            {
                case "1":
                    results = booksInLibrary.FindAll(delegate (Book b) { return b.Author == filterBy; });
                    break;
                case "2":
                    results = booksInLibrary.FindAll(delegate (Book b) { return b.Category == filterBy; });
                    break;
                case "3":
                    results = booksInLibrary.FindAll(delegate (Book b) { return b.Language == filterBy; });
                    break;
                case "4":
                    results = booksInLibrary.FindAll(delegate (Book b) { return b.ISBN == filterBy; });
                    break;
                case "5":
                    results = booksInLibrary.FindAll(delegate (Book b) { return b.Name == filterBy; });
                    break;
                case "6":
                    results = booksInLibrary.FindAll(delegate (Book b) { return b.Taken == (filterBy.ToLower() == "true" ? true : false); });
                    break;
                default:
                    Console.WriteLine("input not recognized");
                    return;
            }
            PrintLibrary(results);
        }

        public static void DeleteBook()
        {
            PrintLibrary();
            Console.WriteLine("select a book to delete (write it's NR to console):");
            int nr;
            try
            {
                nr = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("please write an integer (number)");
                return;
            }
            DeleteBookFromLibrary(nr);
        }

        public static bool DeleteBookFromLibrary(int nr)
        {
            try
            {
                return booksInLibrary.Remove(booksInLibrary[nr]);
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
                return false;
                //throw;
            }
            
        }

        public static void SaveLibrary()
        {
            string jsonString = JsonConvert.SerializeObject(booksInLibrary);
            //Console.WriteLine(jsonString);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
