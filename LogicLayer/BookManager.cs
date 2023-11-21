using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayerInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class BookManager : IBookManager
    {
        private IBookAccessor _bookAccessor = null;

        public BookManager()
        {
            _bookAccessor = new DataAccessLayer.BookAccessor();
        }

        public BookManager(IBookAccessor bookAccessor)
        {
            _bookAccessor = bookAccessor;
        }

        public bool AddBook(Book newBook)
        {
            bool success = false;
            try
            {
                success = (1 == _bookAccessor.InsertBook(newBook));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }

        public bool EditBook(BookVM oldBook, BookVM newBook)
        {
            bool success = false;
            if (1 == _bookAccessor.UpdateBook(oldBook, newBook))
            {
                success = true;
            }
            return success;
        }

        public List<BookVM> RetrieveAllBookVMs()
        {
            /*   var books = new List<BookVM>();
               books.Add(new BookVM());
               books.Add(new BookVM());
               books.Add(new BookVM());
            */

            List<BookVM> books = null;
            try
            {
                books = _bookAccessor.SelectAllBooks();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.", ex);
            }
            return books;
        }

        public List<BookVM> RetrieveBookVMsByCategory(string category)
        {
            /*  var books = new List<BookVM>();
              books.Add(new BookVM());
              books.Add(new BookVM());
              return books;
            */

            List<BookVM> books = null;
            try
            {
                books = _bookAccessor.SelectBooksByBookCategory(category);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.",ex);
            }
            return books;
        }

        public List<BookVM> RetrieveBookVMsByCondition(string condition)
        {
            /*
                var books = new List<BookVM>();
                books.Add(new BookVM());
                books.Add(new BookVM());
                books.Add(new BookVM());
                return books;
            */

            List<BookVM> books = null;
            try
            {
                books = _bookAccessor.SelectBooksByBookCondition(condition);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.", ex);
            }
            return books;
        }

        public List<BookVM> RetrieveBookVMsByGenre(string genre)
        {
            /*
                var books = new List<BookVM>();
                books.Add(new BookVM());
                books.Add(new BookVM());
                return books;
           */

            List<BookVM> books = null;
            try
            {
                books = _bookAccessor.SelectBooksByBookGenre(genre);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Data not found.", ex);
            }
            return books;
        }

        public int AddAuthor(Author newAuthor)
        {
            int newAuthorID = 0;
            try
            {
                newAuthorID = _bookAccessor.InsertAuthor(newAuthor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newAuthorID;
        }

        public bool EditAuthor(Author oldAuthor, Author newAuthor)
        {
            bool success = false;
            if (1 == _bookAccessor.UpdateAuthor(oldAuthor, newAuthor))
            {
                success = true;
            }
            return success;
        }

        public BookVM PopulateAuthorsOnBookVM(BookVM bookVM)
        {
            /*
            bookVM.Authors = new List<Author>();
            Author author1 = new Author();
            Author author2 = new Author();
            bookVM.Authors.Add(author1);
            bookVM.Authors.Add(author2);

            return bookVM;
            */

            try
            {
                bookVM.Authors =
                    _bookAccessor.SelectAuthorsByISBN(bookVM.ISBN);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Authors not found", ex);
            }

            return bookVM;
        }

        public Author RetrieveAuthorByAuthorID(int authorID)
        {
            try
            {
                return _bookAccessor.SelectAuthorByAuthorID(authorID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not found.",ex);
            }
        }

        public bool AddBookAuthor(string ISBN, int authorID)
        {
            bool result = false;
            try
            {
                result = (1 == _bookAccessor.InsertBookAuthor(ISBN, authorID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool EditBookAuthor(string oldISBN, string newISBN, int oldAuthorID, int newAuthorID)
        {
            bool success = false;
            if (1 == _bookAccessor.UpdateBookAuthor(oldISBN, newISBN, oldAuthorID, newAuthorID))
            {
                success = true;
            }
            return success;
        }

        public bool DeleteBookAuthor(string ISBN, int authorID)
        {
            bool success = false;
            try
            {
                success = (1 == _bookAccessor.DeleteBookAuthor(ISBN, authorID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }

        public List<Classification> RetrieveAllBookCategories()
        {
            try
            {
                return _bookAccessor.SelectAllBookCategories();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<Classification> RetrieveAllBookConditions()
        {
            try
            {
                return _bookAccessor.SelectAllBookConditions();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Classification> RetrieveAllBookGenres()
        {
            try
            {
                return _bookAccessor.SelectAllBookGenres();
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
