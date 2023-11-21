using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayerInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class BookAccessor : IBookAccessor
    {
        public int InsertBook(Book newBook)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_insert_book";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@ISBN", newBook.ISBN);
            cmd.Parameters.AddWithValue("@Title", newBook.Title);
            cmd.Parameters.AddWithValue("@BookCategoryID", newBook.BookCategoryID);
            cmd.Parameters.AddWithValue("@BookConditionID", newBook.BookConditionID);
            cmd.Parameters.AddWithValue("@BookGenreID", newBook.BookGenreID);
            cmd.Parameters.AddWithValue("@Publisher", newBook.Publisher);
            cmd.Parameters.AddWithValue("@PublicationDate", newBook.PublicationDate);
            cmd.Parameters.AddWithValue("@WholesalePrice", newBook.WholesalePrice);
            cmd.Parameters.AddWithValue("@SalePrice", newBook.SalePrice);
            cmd.Parameters.AddWithValue("@Quantity", newBook.Quantity);
            cmd.Parameters.AddWithValue("@QuantityByTitle", newBook.QuantityByTitle);
            cmd.Parameters.AddWithValue("@LocationID", newBook.LocationID);
            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public int UpdateBook(BookVM oldBook, BookVM newBook)
        {
            int rows = 0;

            
            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_update_book";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@ISBN", oldBook.ISBN);
            cmd.Parameters.AddWithValue("@OldTitle", oldBook.Title);
            cmd.Parameters.AddWithValue("@OldBookCategoryID", oldBook.BookCategoryID);
            cmd.Parameters.AddWithValue("@OldBookConditionID", oldBook.BookConditionID);
            cmd.Parameters.AddWithValue("@OldBookGenreID", oldBook.BookGenreID);
            cmd.Parameters.AddWithValue("@OldPublisher", oldBook.Publisher);
            cmd.Parameters.AddWithValue("@OldPublicationDate", oldBook.PublicationDate);
            cmd.Parameters.AddWithValue("@OldWholesalePrice", oldBook.WholesalePrice);
            cmd.Parameters.AddWithValue("@OldSalePrice", oldBook.SalePrice);
            cmd.Parameters.AddWithValue("@OldQuantity", oldBook.Quantity);
            cmd.Parameters.AddWithValue("@OldQuantityByTitle", oldBook.QuantityByTitle);
            cmd.Parameters.AddWithValue("@OldLocationID", oldBook.LocationID);
            cmd.Parameters.AddWithValue("@OldActive", oldBook.Active);

            cmd.Parameters.AddWithValue("@NewTitle", newBook.Title);
            cmd.Parameters.AddWithValue("@NewBookCategoryID", newBook.BookCategoryID);
            cmd.Parameters.AddWithValue("@NewBookConditionID", newBook.BookConditionID);
            cmd.Parameters.AddWithValue("@NewBookGenreID", newBook.BookGenreID);
            cmd.Parameters.AddWithValue("@NewPublisher", newBook.Publisher);
            cmd.Parameters.AddWithValue("@NewPublicationDate", newBook.PublicationDate);
            cmd.Parameters.AddWithValue("@NewWholesalePrice", newBook.WholesalePrice);
            cmd.Parameters.AddWithValue("@NewSalePrice", newBook.SalePrice);
            cmd.Parameters.AddWithValue("@NewQuantity", newBook.Quantity);
            cmd.Parameters.AddWithValue("@NewQuantityByTitle", newBook.QuantityByTitle);
            cmd.Parameters.AddWithValue("@NewLocationID", newBook.LocationID);
            cmd.Parameters.AddWithValue("@NewActive", newBook.Active);

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public List<BookVM> SelectAllBooks()
        {
            List<BookVM> books = new List<BookVM>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_books";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var book = new BookVM();
                        book.ISBN = reader.GetString(0);
                        book.Title = reader.GetString(1);
                        book.BookCategoryID = reader.GetString(2);
                        book.BookConditionID = reader.GetString(3);
                        book.BookGenreID = reader.GetString(4);
                        book.Publisher = reader.GetString(5);
                        book.PublicationDate = reader.GetDateTime(6);
                        book.WholesalePrice = reader.GetDecimal(7);
                        book.SalePrice = reader.GetDecimal(8);
                        book.Quantity = reader.GetInt32(9);
                        book.QuantityByTitle = reader.GetInt32(10);
                        book.LocationID = reader.GetString(11);
                        book.Active = reader.GetBoolean(12);

                        books.Add(book);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return books;
        }

        public List<BookVM> SelectBooksByBookCategory(string bookCategory)
        {
            List<BookVM> books = new List<BookVM>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_books_by_bookCategoryID";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@BookCategoryID", SqlDbType.NVarChar, 25);

            // values
            cmd.Parameters["@BookCategoryID"].Value = bookCategory;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
     //                   [ISBN], [Title], [BookCategoryID], [BookConditionID], [BookGenreID],[Publisher],
     //                   [PublicationDate], [SalePrice], [Quantity], [QuantityByTitle], [LocationID]
                        var book = new BookVM();
                        book.ISBN = reader.GetString(0);
                        book.Title = reader.GetString(1);
                        book.BookCategoryID = reader.GetString(2);
                        book.BookConditionID = reader.GetString(3);
                        book.BookGenreID = reader.GetString(4);
                        book.Publisher = reader.GetString(5);
                        book.PublicationDate = reader.GetDateTime(6);
                        book.WholesalePrice = reader.GetDecimal(7);
                        book.SalePrice = reader.GetDecimal(8);
                        book.Quantity = reader.GetInt32(9);
                        book.QuantityByTitle = reader.GetInt32(10);
                        book.LocationID = reader.GetString(11);
                        book.Active = reader.GetBoolean(12);

                        books.Add(book);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return books;
        }

        public List<BookVM> SelectBooksByBookCondition(string bookCondition)
        {
            List<BookVM> books = new List<BookVM>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_books_by_bookConditionID";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@BookConditionID", SqlDbType.NVarChar, 25);

            // values
            cmd.Parameters["@BookConditionID"].Value = bookCondition;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var book = new BookVM();
                        book.ISBN = reader.GetString(0);
                        book.Title = reader.GetString(1);
                        book.BookCategoryID = reader.GetString(2);
                        book.BookConditionID = reader.GetString(3);
                        book.BookGenreID = reader.GetString(4);
                        book.Publisher = reader.GetString(5);
                        book.PublicationDate = reader.GetDateTime(6);
                        book.WholesalePrice = reader.GetDecimal(7);
                        book.SalePrice = reader.GetDecimal(8);
                        book.Quantity = reader.GetInt32(9);
                        book.QuantityByTitle = reader.GetInt32(10);
                        book.LocationID = reader.GetString(11);
                        book.Active = reader.GetBoolean(12);

                        books.Add(book);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return books;
        }

        public List<BookVM> SelectBooksByBookGenre(string bookGenre)
        {
            List<BookVM> books = new List<BookVM>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_books_by_bookGenreID";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@BookGenreID", SqlDbType.NVarChar, 25);

            // values
            cmd.Parameters["@BookGenreID"].Value = bookGenre;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var book = new BookVM();
                        book.ISBN = reader.GetString(0);
                        book.Title = reader.GetString(1);
                        book.BookCategoryID = reader.GetString(2);
                        book.BookConditionID = reader.GetString(3);
                        book.BookGenreID = reader.GetString(4);
                        book.Publisher = reader.GetString(5);
                        book.PublicationDate = reader.GetDateTime(6);
                        book.WholesalePrice = reader.GetDecimal(7);
                        book.SalePrice = reader.GetDecimal(8);
                        book.Quantity = reader.GetInt32(9);
                        book.QuantityByTitle = reader.GetInt32(10);
                        book.LocationID = reader.GetString(11);
                        book.Active = reader.GetBoolean(12);

                        books.Add(book);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return books;
        }

        public int InsertAuthor(Author newAuthor)
        {
            int authorID = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_insert_author";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@GivenName", newAuthor.GivenName);
            cmd.Parameters.AddWithValue("@FamilyName", newAuthor.FamilyName);
            try
            {
                conn.Open();

                authorID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return authorID;
        }

        public int UpdateAuthor(Author oldAuthor, Author newAuthor)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_update_author";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@OldAuthorID", oldAuthor.AuthorID);
            cmd.Parameters.AddWithValue("@OldGivenName", oldAuthor.GivenName);
            cmd.Parameters.AddWithValue("@OldFamilyName", oldAuthor.FamilyName);

            cmd.Parameters.AddWithValue("@NewGivenName", newAuthor.GivenName);
            cmd.Parameters.AddWithValue("@NewFamilyName", newAuthor.FamilyName);

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public List<Author> SelectAuthorsByISBN(string ISBN)
        {
            List<Author> authors = new List<Author>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_authors_by_isbn";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.Add("@ISBN", SqlDbType.NVarChar, 50);

            // values
            cmd.Parameters["@ISBN"].Value = ISBN;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var author = new Author();
                        author.AuthorID = reader.GetInt32(0);
                        author.GivenName = reader.GetString(1);
                        author.FamilyName = reader.GetString(2);

                        authors.Add(author);
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid ISBN");
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return authors;
        }

        public Author SelectAuthorByAuthorID(int authorID)
        {
            Author author = new Author();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_author_by_AuthorID";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@AuthorID", authorID); 

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        author.AuthorID = reader.GetInt32(0);
                        author.GivenName = reader.GetString(1);
                        author.FamilyName = reader.GetString(2);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return author;
        }

        public int InsertBookAuthor(string ISBN, int authorID)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_insert_bookAuthor";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@ISBN", ISBN);
            cmd.Parameters.AddWithValue("@AuthorID", authorID);
           
            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public int UpdateBookAuthor(string oldISBN, string newISBN, int oldAuthorID, int newAuthorID)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_update_bookAuthor";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@OldISBN", oldISBN);
            cmd.Parameters.AddWithValue("@OldAuthorID", oldAuthorID);
            cmd.Parameters.AddWithValue("@NewISBN", newISBN);
            cmd.Parameters.AddWithValue("@NewAuthorID", newAuthorID);

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public int DeleteBookAuthor(string ISBN, int authorID)
        {
            int rows = 0;

            // connection
            DBConnection connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // cmdText
            var cmdText = "sp_delete_bookAuthor";

            //command
            var cmd = new SqlCommand(cmdText, conn);

            // type
            cmd.CommandType = CommandType.StoredProcedure;

            // parameters
            cmd.Parameters.AddWithValue("@ISBN", ISBN);
            cmd.Parameters.AddWithValue("@AuthorID", authorID);

            try
            {
                conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public List<Classification> SelectAllBookCategories()
        {
            List<Classification> categories = new List<Classification>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_book_categories";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var category = new Classification();
                        category.Name = reader.GetString(0);
                        category.Description = reader.GetString(1);
                        categories.Add(category);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return categories;
        }

        public List<Classification> SelectAllBookConditions()
        {
            List<Classification> conditions = new List<Classification>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_book_conditions";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var condition = new Classification();
                        condition.Name = reader.GetString(0);
                        condition.Description = reader.GetString(1);
                        conditions.Add(condition);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return conditions;
        }

        public List<Classification> SelectAllBookGenres()
        {
            List<Classification> genres = new List<Classification>();

            // connection
            var connectionFactory = new DBConnection();
            var conn = connectionFactory.GetConnection();

            // command text
            var cmdText = "sp_select_all_book_genres";

            // command
            var cmd = new SqlCommand(cmdText, conn);

            // command type
            cmd.CommandType = CommandType.StoredProcedure;

            // try-catch-finally
            try
            {
                // open a connection
                conn.Open();

                // execute command
                var reader = cmd.ExecuteReader();

                // process the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var genre = new Classification();
                        genre.Name = reader.GetString(0);
                        genre.Description = reader.GetString(1);
                        genres.Add(genre);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return genres;
        }

        
    }
}
