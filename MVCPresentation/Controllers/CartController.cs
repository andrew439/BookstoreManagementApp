using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using LogicLayer.Concrete;
using LogicLayerInterfaces;
using MVCPresentation.Models;
using System.ComponentModel.DataAnnotations;

namespace MVCPresentation.Controllers
{
    
    public class CartController : Controller
    {
        private IBookManager _bookManager;
        private IOrderProcessor _orderProcessor;
        private IEnumerable<BookVM> _bookVMs = new List<BookVM>();
        private EmailSettings emailSettings = new EmailSettings();

        public CartController()
        {
            _bookManager = new BookManager();
            _orderProcessor = new EmailOrderProcessor(emailSettings);
            PopulateBookList();
        }

        public CartController(IBookManager bookManager, IOrderProcessor proc)
        {
            _bookManager = bookManager;
            _orderProcessor = proc;
            PopulateBookList();
        }

        private void PopulateBookList()
        {
            try
            {
                _bookVMs = _bookManager.RetrieveAllBookVMs();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                RedirectToAction("Error");
            }
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        [Authorize]
        public RedirectToRouteResult AddToCart(Cart cart, string ISBN, string returnUrl)
        {

            BookVM bookVM = _bookVMs.FirstOrDefault(b => b.ISBN == ISBN);

            if (bookVM != null)
            {
                // GetCart().AddItem(product, 1);
                cart.AddItem(bookVM, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, string ISBN, string returnUrl)
        {
            BookVM bookVM = _bookVMs.FirstOrDefault(b => b.ISBN == ISBN);

            if (bookVM != null)
            {
                // GetCart().RemoveLine(product);
                cart.RemoveLine(bookVM);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}