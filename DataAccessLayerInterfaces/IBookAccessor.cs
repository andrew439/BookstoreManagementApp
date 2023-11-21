using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessLayerInterfaces
{
    public interface IBookAccessor
    {
        int InsertBook(Book newBook);

        int UpdateBook(BookVM oldBook, BookVM newBook);

        List<BookVM> SelectAllBooks();

        List<BookVM> SelectBooksByBookCategory(string bookCategory);

        List<BookVM> SelectBooksByBookCondition(string bookCondition);

        List<BookVM> SelectBooksByBookGenre(string bookGenre);

        int InsertAuthor(Author newAuthor);

        int UpdateAuthor(Author oldAuthor, Author newAuthor);

        List<Author> SelectAuthorsByISBN(string ISBN);

        int InsertBookAuthor(string ISBN, int authorID);

        int UpdateBookAuthor(string oldISBN, string newISBN, int oldAuthorID, int newAuthorID);

        int DeleteBookAuthor(string ISBN, int authorID);

        Author SelectAuthorByAuthorID(int authorID);

        List<Classification> SelectAllBookCategories();

        List<Classification> SelectAllBookConditions();

        List<Classification> SelectAllBookGenres();
    }
}
