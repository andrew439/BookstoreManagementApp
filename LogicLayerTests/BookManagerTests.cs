using DataAccessLayerFakes;
using System.Collections.Generic;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using System;

namespace LogicLayerTests
{
    [TestClass]
    public class BookManagerTests
    {
        private BookManager _bookManager = null;

        [TestInitialize]
        public void TestSetup()
        {
 //           _bookManager = new BookManager(new DataAccessLayer.BookAccessor());
            _bookManager = new BookManager(new BookAccessorFake());
        }

        [TestMethod]
        public void TestRetrieveAllBooks()
        {
            // Arrange
            int expectedCount = 3;
            int actualCount = 0;

            // Act
            var books = _bookManager.RetrieveAllBookVMs();
            actualCount = books.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveBooksByCategoryReturnsCorrectList()
        {
            // Arrange
            string bookCategory = "Nonfiction";
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            var books = _bookManager.RetrieveBookVMsByCategory(bookCategory);
            actualCount = books.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveBooksByConditionReturnsCorrectList()
        {
            // Arrange
            string bookCondition = "New";
            int expectedCount = 3;
            int actualCount = 0;

            // Act
            var books = _bookManager.RetrieveBookVMsByCondition(bookCondition);
            actualCount = books.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveBooksByGenreReturnsCorrectList()
        {
            // Arrange
            string bookGenre = "Textbook";
            int expectedCount = 2;
            int actualCount = 0;

            // Act
            var books = _bookManager.RetrieveBookVMsByGenre(bookGenre);
            actualCount = books.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestPopulateAuthorsOnBookVMsReturnsCorrectList()
        {
            // arrange
            const string isbn = "111-2";
            const int expectedCount = 2;
            BookVM bookVM = new BookVM() { ISBN = isbn };
            int actualCount;

            // act
            bookVM = _bookManager.PopulateAuthorsOnBookVM(bookVM);
            actualCount = bookVM.Authors.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        
    }
}
