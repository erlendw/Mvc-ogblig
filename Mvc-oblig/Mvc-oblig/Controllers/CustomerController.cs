using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_oblig.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult ListOfAllCustomers()
        {
            var db = new Models.CustomerContext();
            List<Models.Customer> ListOfAllCustomers = db.Customer.ToList();
            ViewData.Model = ListOfAllCustomers;
            ViewData["Message"] = "Do some stuff..and some shit: ";
            return View();
        }
    }
}