using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;

namespace MVCPresentation.Controllers
{
    public class CustomersController : Controller
    {
        ICustomerManager _customerManager = null;

        public CustomersController()
        {
            _customerManager = new CustomerManager();
        }

        public CustomersController(ICustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        
        /// <summary>
        /// Andrew Schneider
        /// Created: 2023/05/04
        /// 
        /// Controller method for /Customers/Controller to view Customers
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <returns>Customers/Index view</returns>
        public ActionResult Index()
        {
            IEnumerable<Customer> customers = _customerManager.RetrieveAllActiveCustomers();
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
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

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customers/Edit/5
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

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customers/Delete/5
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
