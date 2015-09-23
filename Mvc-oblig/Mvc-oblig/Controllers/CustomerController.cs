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
        public ActionResult getAllCustomers()
        {
            return View();
        }
    }
}