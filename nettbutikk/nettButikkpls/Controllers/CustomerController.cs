using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using nettButikkpls.BLL;
using nettButikkpls.Models;


namespace nettButikkpls.Controllers
{
    public class CustomerController : Controller
    {
        //NettbutikkContext bmx = new NettbutikkContext();
        public ActionResult List()
        {
            var db = new CustomerBLL();
            List<Customer> allCustomers = db.allCustomers();
            return View(allCustomers);
        }
        public ActionResult Reg()
        {
            return View();
        }
        public ActionResult CurrentCustomer()
        {
            var db = new CustomerBLL();
            return View(db.CurrentCustomerObj());
        }

        [HttpPost]
        public ActionResult Reg(Customer inCustomer)
        {
            var db = new CustomerBLL();
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
            var db = new CustomerBLL();
            bool OK = db.EditCustomer(inList);
            if(OK)
            {
                return RedirectToAction("List");
            }
            return View();
        }
        public Customer FindCustomerByEmail (string Email)
        {
            var db = new CustomerBLL();
            //List<Customer> GetAllCustomers = bmx.Customers.ToList();
            List<Customer> GetAllCustomers = db.allCustomers();
            for (int i = 0; i < GetAllCustomers.Count; i++)
            {
                if(GetAllCustomers[i].email == Email)
                {
                    return GetAllCustomers[i];
                }
            }
            return null;
        }
        public ActionResult Login()
        {
            var db = new CustomerBLL();
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
            var db = new CustomerBLL();
            bool loggedIn = db.ValidateUser(inList);
            if (loggedIn)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Login");//Implisitt else
        }
    }

}