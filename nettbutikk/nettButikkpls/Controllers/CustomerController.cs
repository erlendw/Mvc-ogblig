using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;
using System.Diagnostics;


namespace nettButikkpls.Controllers
{
    public class CustomerController : Controller
    {
        NettbutikkContext bmx = new NettbutikkContext();
        public ActionResult List()
        {
            var db = new DbCustomer();
            List<Customer> allCustomers = db.allCustomers();
            return View(allCustomers);
        }
        public ActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reg(Customer inCustomer)
        {
            var db = new DbCustomer();
            bool OK = db.saveCustomer(inCustomer);
            if (OK)
            {
                return RedirectToAction("List");
            }
            return View();
        }
        public ActionResult UserProfile()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
            
        }
        [HttpPost]
        public ActionResult UpdateCustomer(FormCollection inList)
        {
            var db = new DbCustomer();
            bool OK = db.EditCustomer(inList);
            if(OK)
            {
                return RedirectToAction("List");
            }
            return View();
        }
        public Customers FindCustomerByEmail (string Email)
        {
            List<Customers> GetAllCustomers = bmx.Customers.ToList();
            for(int i = 0; i < GetAllCustomers.Count; i++)
            {
                if(GetAllCustomers[i].Mail == Email)
                {
                    return GetAllCustomers[i];
                }
            }
            return null;
        }
        public ActionResult Login()
        {
            var db = new DbCustomer();
            bool loggedIn = db.Login();
            if (loggedIn)
            {
                ViewBag.loggedin = true; //Forklar meg dette den so lagde det
                return RedirectToAction("List");
            }
            return View(); //Implisitt else
        }
        [HttpPost]
        public ActionResult ValidateUser(FormCollection inList)
        {
            //Trenger feilmelding når brukervalidering feiler.
            var db = new DbCustomer();
            bool loggedIn = db.ValidateUser(inList);
            if (loggedIn)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Login");//Implisitt else
    }

    }
}