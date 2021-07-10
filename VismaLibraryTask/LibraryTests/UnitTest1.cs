using System;
using System.Text.Json;
using VismaLibraryTask;
using Xunit;
using static VismaLibraryTask.Program;


namespace LibraryTests
{
    public class UnitTest1
    {
        
        [Fact]
        public void CreateBook()
        {
            Book testBook = new Book("Test", "author", "testing","English", DateTime.Now, "0-000-000-000-001");
            Assert.NotNull(testBook);
        }
        
        [Fact]
        public void AddBook()
        {
            ReadFromFile();
            Book testBook = new Book("UnitTest", "author", "testing", "English", DateTime.Now, "0-000-000-000-123");
            AddBookToLibrary(testBook);
            Assert.Contains<Book>(testBook, booksInLibrary);
        }

        [Fact]
        public void RemoveBook()
        {
            ReadFromFile();
            Book testBook = new Book("UnitTest", "author", "testing", "English", DateTime.Now, "0-000-000-000-123");
            AddBookToLibrary(testBook);
            Assert.True(DeleteBookFromLibrary(booksInLibrary.Count - 1));
            Assert.DoesNotContain<Book>(testBook, booksInLibrary); //book was deleted
            //Assert.Throws<ArgumentOutOfRangeException>(()=>DeleteBookFromLibrary(booksInLibrary.Count + 1));
            Assert.False(DeleteBookFromLibrary(booksInLibrary.Count + 1)); //book not found
        }

        [Fact]
        public void TakeBook()
        {
            ReadFromFile();
            Book testBook = new Book("UnitTest", "author", "testing", "English", DateTime.Now, "0-000-000-000-123");
            AddBookToLibrary(testBook);
            Assert.False(TakeBookFromLibrary(booksInLibrary.Count - 1, "UnitTest", 2, 1)); //borrowing time too long
            Assert.True(TakeBookFromLibrary(booksInLibrary.Count - 1, "UnitTest", 1, 15)); //can take book
            Assert.False(TakeBookFromLibrary(booksInLibrary.Count - 1, "UnitTest", 0, 45)); //book already borrowed
        }

        [Fact]
        public void ReturnBook()
        {
            ReadFromFile();
            Book testBook = new Book("UnitTest", "author", "testing", "English", DateTime.Now, "0-000-000-000-123");
            AddBookToLibrary(testBook);
            Assert.True(TakeBookFromLibrary(booksInLibrary.Count - 1, "UnitTest", 1, 5));
            Assert.True(ReturnBookToLibrary(booksInLibrary.Count - 1)); //can return book
            Assert.False(ReturnBookToLibrary(booksInLibrary.Count - 1)); //book already returned, can't return it again
        }
    }
}
