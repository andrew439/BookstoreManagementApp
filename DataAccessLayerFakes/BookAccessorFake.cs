using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayerFakes
{
    public class BookAccessorFake : IBookAccessor
    {
        List<BookVM> bookVMs = new List<BookVM>();
        List<Author> fakeAuthors = new List<Author>();
        Author fakeAuthor = new Author();

        public BookAccessorFake()
        {
            bookVMs.Add(new BookVM
            {
                ISBN = "111-1",
                Title = "Test Book",
                BookCategoryID = "Nonfiction",
                BookConditionID = "New",
                BookGenreID = "Textbook",
                Publisher = "Test Publishing",
                PublicationDate = DateTime.Parse("2022-11-10"),
                SalePrice = 9.99M,
                Quantity = 1,
                QuantityByTitle = 1,
                LocationID = "Test",
                Authors = new List<Author>()
            });

            bookVMs.Add(new BookVM
            {
                ISBN = "111-2",
                Title = "Test Book 2",
                BookCategoryID = "Nonfiction",
                BookConditionID = "New",
                BookGenreID = "Textbook",
                Publisher = "Test Publishing",
                PublicationDate = DateTime.Parse("2022-11-10"),
                SalePrice = 9.99M,
                Quantity = 1,
                QuantityByTitle = 1,
                LocationID = "Test 2",
                Authors = new List<Author>()
            });

            bookVMs.Add(new BookVM
            {
                ISBN = "111-3",
                Title = "Test Book 3",
                BookCategoryID = "Fiction",
                BookConditionID = "New",
                BookGenreID = "Sci-fi",
                Publisher = "Test Publishing",
                PublicationDate = DateTime.Parse("2022-11-19"),
                SalePrice = 9.99M,
                Quantity = 1,
                QuantityByTitle = 1,
                LocationID = "Test 3",
                Authors = new List<Author>()
            });

            bookVMs[0].Authors = new List<Author>();
            Author author1 = new Author();
            Author author2 = new Author();
            bookVMs[0].Authors.Add(author1);
            bookVMs[0].Authors.Add(author2);

            fakeAuthors.Add(new Author
            {
                AuthorID = 1,
                GivenName = "Test",
                FamilyName = "Person1"
            });

            fakeAuthors.Add(new Author
            {
                AuthorID = 2,
                GivenName = "Test",
                FamilyName = "Person2"
            });

        }

        public int InsertBook(Book newBook)
        {
            throw new NotImplementedException();
        }

        public int UpdateBook(BookVM oldBook, BookVM newBook)
        {
            throw new NotImplementedException();
        }

        public List<BookVM> SelectAllBooks()
        {
            return bookVMs;
        }

        public List<BookVM> SelectBooksByBookCategory(string bookCategory)
        {
            return bookVMs.Where(b => b.BookCategoryID == bookCategory).ToList();
        }

        public List<BookVM> SelectBooksByBookCondition(string bookCondition)
        {
            return bookVMs.Where(b => b.BookConditionID == bookCondition).ToList();
        }

        public List<BookVM> SelectBooksByBookGenre(string bookGenre)
        {
            return bookVMs.Where(b => b.BookGenreID == bookGenre).ToList();
        }

        public int InsertAuthor(Author newAuthor)
        {
            throw new NotImplementedException();
        }

        public int UpdateAuthor(Author oldAuthor, Author newAuthor)
        {
            throw new NotImplementedException();
        }

        public List<Author> SelectAuthorsByISBN(string ISBN)
        {
            var authors = new List<Author>();

            foreach (var c in bookVMs)
            {
                if (c.Authors != null)
                {
                    authors = c.Authors;
                    break;
                }
            }

            return authors;
        }

        public Author SelectAuthorByAuthorID(int authorID)
        {
            throw new NotImplementedException();
        }

        public int InsertBookAuthor(string ISBN, int authorID)
        {
            throw new NotImplementedException();
        }

        public int UpdateBookAuthor(string oldISBN, string newISBN, int oldAuthorID, int newAuthorID)
        {
            throw new NotImplementedException();
        }

        public int DeleteBookAuthor(string ISBN, int authorID)
        {
            throw new NotImplementedException();
        }

        public List<Classification> SelectAllBookCategories()
        {
            throw new NotImplementedException();
        }

        public List<Classification> SelectAllBookConditions()
        {
            throw new NotImplementedException();
        }

        public List<Classification> SelectAllBookGenres()
        {
            throw new NotImplementedException();
        }

    }
}
