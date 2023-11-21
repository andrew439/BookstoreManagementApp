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
    public class HomeController : Controller
    {
        private IBookManager _bookManager;

        public HomeController()
        {
            _bookManager = new BookManager();
        }

        public HomeController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public ActionResult Index()
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

            bookVMs = bookVMs.Where(b => b.Active != false && b.BookConditionID == "New").OrderBy(b => b.ISBN);

            return View(bookVMs);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Our Story";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We'd love to hear from you!";

            return View();
        }
    }
}