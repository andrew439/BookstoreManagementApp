using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayerInterfaces;
using LogicLayer;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        IBookManager _bookManager = null;

        public InventoryController()
        {
            _bookManager = new BookManager();
        }

        public InventoryController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        /// <summary>
        /// Author: Andrew Schneider
        /// Date: 2023/05/01
        /// 
        /// Controller method for /Books/Index to view a list of books
        /// </summary>
        /// <param name="booksViewModel"></param>
        /// <returns>Books/Index view</returns>
        public ActionResult Index(BooksViewModel booksViewModel)
        {
            List<BookVM> bookVMs = new List<BookVM>();
            try
            {
                bookVMs = _bookManager.RetrieveAllBookVMs();

                for (int i = 0; i < bookVMs.Count(); i++)
                {
                    bookVMs[i] = _bookManager.PopulateAuthorsOnBookVM(bookVMs[i]);
                }

                foreach (var book in bookVMs)
                {
                    if(book.Title.Length > 25)
                    {
                        book.Title = book.Title.Substring(0, 25) + "...";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }

            try
            {
                booksViewModel.ConditionOptions = _bookManager.RetrieveAllBookConditions();
                booksViewModel.GenreOptions = _bookManager.RetrieveAllBookGenres();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.InnerException.Message;
                return View("Error");
            }

            try
            {
                if (booksViewModel.Condition != null)
                {
                    bookVMs = _bookManager.RetrieveBookVMsByCondition(booksViewModel.Condition);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }

            try
            {
                if (booksViewModel.Genre != null)
                {
                    bookVMs = _bookManager.RetrieveBookVMsByGenre(booksViewModel.Genre);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + "\n\n" + ex.Message;
                return View("Error");
            }

            switch (booksViewModel.CategoryFilterOptions)
            {
                case null:
                    break;
                case BookCategoryFilterOptions.Fiction:
                    bookVMs = bookVMs.Where(b => b.BookCategoryID == "Fiction").ToList();
                    break;
                case BookCategoryFilterOptions.Nonfiction:
                    bookVMs = bookVMs.Where(b => b.BookCategoryID == "Nonfiction").ToList();
                    break;
                default:
                    break;
            }

            switch (booksViewModel.Sort)
            {
                case null:
                    break;
                //case BookSortOptions.Author:
                //    bookVMs = bookVMs.OrderBy(b => b.Authors[0]).ToList();
                //    break;
                case BookSortOptions.Title:
                    bookVMs = bookVMs.OrderBy(b => b.Title).ToList();
                    break;
                case BookSortOptions.Price:
                    bookVMs = bookVMs.OrderBy(b => b.SalePrice).ToList();
                    break;
            }

            booksViewModel.BookVMs = bookVMs;
            booksViewModel.Count = booksViewModel.BookVMs.Count();
            return View(booksViewModel);
        }

        /// <summary>
        /// Author: Andrew Schneider
        /// Date: 2023/05/01
        /// 
        /// Controller method for /Books/Details to view details of books
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns>Books/Details view</returns>
        public ActionResult Details(string ISBN)
        {
            if (ISBN != null) // not null?
            {
                BookVM bookVM = new BookVM();
                try
                {
                    bookVM = _bookManager.RetrieveAllBookVMs().Find(b => b.ISBN == ISBN);
                    if (bookVM == null)
                    {
                        ViewBag.Message = "A book with that ISBN was not found.";
                        return View("Error");
                    }

                    _bookManager.PopulateAuthorsOnBookVM(bookVM);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("Error");
                }

                return View(bookVM);
            }
            else
            {
                ViewBag.Message = "You need to specify a book to view.";
                return View("Error");
            }
        }

        /// <summary>
        /// Author: Andrew Schneider
        /// Date: 2023/05/01
        /// 
        /// Controller method for /Books/Create to create books
        /// </summary>
        /// <returns>/Books/Create view</returns>
        [Authorize(Roles = "Administrator, Manager, Purchaser")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.BookCategories = _bookManager.RetrieveAllBookCategories();
                ViewBag.BookGenres = _bookManager.RetrieveAllBookGenres();
                ViewBag.BookConditions = _bookManager.RetrieveAllBookConditions();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
            return View();
        }

        /// <summary>
        /// Author: Andrew Schneider
        /// Date: 2023/05/01
        /// 
        /// Controller method for /Books/Create POST to save new book to database
        /// </summary>
        /// <param name="createbookViewModel"></param>
        /// <returns>view</returns>
        [HttpPost]
        [Authorize(Roles = "Administrator, Manager, Purchaser")]
        public ActionResult Create(CreateBookViewModel createbookViewModel)
        {
            List<int> authorIDs = new List<int>();
            if (ModelState.IsValid)
            {
                try
                {
                    string[] names = createbookViewModel.Authors.Split(',');
                    foreach (var name in names)
                    {
                        string[] separatedName = name.TrimStart().Split(' ');
                        Author author = new Author();
                        author.GivenName = separatedName[0];
                        author.FamilyName = separatedName[1];
                        authorIDs.Add(_bookManager.AddAuthor(author));
                    }
                    _bookManager.AddBook(createbookViewModel.Book);

                    foreach (var id in authorIDs)
                    {
                        _bookManager.AddBookAuthor(createbookViewModel.Book.ISBN, id);
                    }
                    

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return View("Error");
                }

            }
            else // modelstate was not valid
            {
                try     // send the dropdown list data to the view
                {
                    ViewBag.BookCategories = _bookManager.RetrieveAllBookCategories();
                    ViewBag.BookGenres = _bookManager.RetrieveAllBookGenres();
                    ViewBag.BookConditions = _bookManager.RetrieveAllBookConditions();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }

                return View(); // returns view with server-side validation messages
            }
        }

        /// <summary>
        /// Author: Andrew Schneider
        /// Date: 2023/05/01
        /// 
        /// Controller method for /Books/Edit to edit a book
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns>Books/Edit view</returns>
        [Authorize(Roles = "Administrator, Manager, Purchaser")]
        public ActionResult Edit(string ISBN)
        {
            BookVM bookToEdit = null;

            try     // send the dropdown list data to the view
            {
                ViewBag.BookConditions = _bookManager.RetrieveAllBookConditions();

                bookToEdit = _bookManager.RetrieveAllBookVMs().First(b => b.ISBN == ISBN);

                Session["oldBook"] = bookToEdit;

                if (bookToEdit == null)
                {
                    throw new Exception("Book not found.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }

            return View(bookToEdit);
        }

        /// <summary>
        /// Author: Andrew Schneider
        /// Date: 2023/05/01
        /// 
        /// Controller method for /Books/List to save edited book to the database
        /// </summary>
        /// <param name="newBook"></param>
        /// <param name="f"></param>
        /// <returns>view</returns>
        [HttpPost]
        [Authorize(Roles = "Administrator, Manager, Purchaser")]
        public ActionResult Edit(BookVM newBook, FormCollection f)
        {
            BookVM oldBook = (BookVM)Session["oldBook"];


            if (ModelState.IsValid)
            {
                try
                {
                    _bookManager.EditBook(oldBook, newBook);

                    Session["oldBook"] = null;

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Author: Andrew Schneider
        /// Date: 2023/05/01
        /// 
        /// Controller method for /Books/Delete to delete a book
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns>Books/Delete view</returns>
        [Authorize(Roles = "Administrator, Manager, Purchaser")]
        public ActionResult Delete(string ISBN)
        {
            if (ISBN != null) // not null?
            {
                BookVM bookVM = _bookManager.RetrieveAllBookVMs().Find(b => b.ISBN == ISBN);

                if (bookVM == null)
                {
                    ViewBag.Message = "A book with that ISBN was not found.";
                    return View("Error");
                }

                return View(bookVM);
            }
            else
            {
                ViewBag.Message = "You need to specify a book to view.";
                return View("Error");
            }
        }

        /// <summary>
        /// Author: Andrew Schneider
        /// Date: 2023/05/01
        /// 
        /// Controller method for /Books/List to deactivate deleted book
        /// </summary>
        /// <param name="ISBN"></param>
        /// <param name="f"></param>
        /// <returns>view</returns>
        [HttpPost]
        [Authorize(Roles = "Administrator, Manager, Purchaser")]
        public ActionResult Delete(string ISBN, FormCollection f)
        {
            if (ISBN != null)
            {
                try
                {
                    BookVM oldBook = _bookManager.RetrieveAllBookVMs().First(b => b.ISBN == ISBN);
                    BookVM newBook = _bookManager.RetrieveAllBookVMs().First(b => b.ISBN == ISBN);
                    newBook.Active = false;

                    _bookManager.EditBook(oldBook, newBook);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "You need to specify a book to delete.";
                return View("Error");
            }
        }
    }
}
