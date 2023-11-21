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
    public class BooksController : Controller
    {
        private IBookManager _bookManager;
        public int _pageSize = 6;

        public BooksController()
        {
            _bookManager = new BookManager();
        }

        public BooksController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        
        /// <summary>
        /// Author: Andrew Schneider
        /// Date: 2023/05/01
        /// 
        /// Controller method for /Books/List to view a list of books
        /// </summary>
        /// <param name="category"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ViewResult List(string category, int page = 1)
        {
            IEnumerable<BookVM> bookVMs = new List<BookVM>();
            
            try
            {
                bookVMs = _bookManager.RetrieveAllBookVMs();

                foreach (var book in bookVMs)
                {
                    _bookManager.PopulateAuthorsOnBookVM(book);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }

            CustomerBooksViewModel model = new CustomerBooksViewModel
            {
                BookVMs = bookVMs
                .Where(b => category == null || b.BookCategoryID == category && b.BookConditionID != "Not for sale")
                .OrderBy(b => b.ISBN).Skip((page - 1) * _pageSize)
                .Take(_pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = category == null ?
                        bookVMs.Count() :
                        bookVMs.Where(b => b.BookCategoryID == category).Count()
                },
                CurrentCategory = category
            };
            
            if(category == null)
            {
                ViewBag.PageType = "All Categories";
            }
            else
            {
                ViewBag.PageType = category;
            }

            return View(model);
        }


        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Book/Create
        [Authorize(Roles = "Manager, Purchaser")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [Authorize(Roles = "Manager, Purchaser")]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
