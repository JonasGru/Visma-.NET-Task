using System;
using System.Text.Json;
using VismaLibraryTask;
using Xunit;


namespace LibraryTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Fact]
        public void CreateBook()
        {
            //Assert.True(true);
            Book testBook = new Book("Test", "author", "testing","English", DateTime.Now, "0-000-000-000-001");

            Assert.NotNull(testBook);
        }
    }
}
