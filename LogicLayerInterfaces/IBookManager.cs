using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IBookManager
    {
        bool AddBook(Book newBook);

        bool EditBook(BookVM oldBook, BookVM newBook);

        List<BookVM> RetrieveAllBookVMs();

        List<BookVM> RetrieveBookVMsByCategory(string category);

        List<BookVM> RetrieveBookVMsByCondition(string condition);

        List<BookVM> RetrieveBookVMsByGenre(string genre);

        int AddAuthor(Author newAuthor);

        bool EditAuthor(Author oldAuthor, Author newAuthor);

        BookVM PopulateAuthorsOnBookVM(BookVM bookVM);

        Author RetrieveAuthorByAuthorID(int authorID);

        bool AddBookAuthor(string ISBN, int authorID);

        bool EditBookAuthor(string oldISBN, string newISBN, int oldAuthorID, int newAuthorID);

        bool DeleteBookAuthor(string ISBN, int authorID);

        List<Classification> RetrieveAllBookCategories();

        List<Classification> RetrieveAllBookConditions();

        List<Classification> RetrieveAllBookGenres();


    }
}
