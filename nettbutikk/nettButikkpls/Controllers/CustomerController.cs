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
            if (Session["loggedin"] == null)
            {
                Session["loggedin"] = false;
            }
            else
            {
                ViewBag.loggedin = (bool)Session["loggedin"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult ValidateUser(FormCollection inList)
        {
            Customers customer = FindCustomerByEmail(inList["Email"]);

            var dbCm = new DbCustomer();
            if(customer != null)
            {
                String OldHash = customer.Password;
                String ReHash = dbCm.HashPassword(inList["Password"], customer.Salt);

                if(OldHash == ReHash)
                {
                    Session["loggedin"] = true;
                    Session["CurrentUser"] = customer;
                    Debug.WriteLine("Du er nå logget inn");
                    return RedirectToAction("List");
                }
                else
                {
                    Session["loggedin"] = false;
                    Session["CurrentUser"] = null;
                    Debug.WriteLine("Kunne ikke logge inn");
                    return RedirectToAction("Reg");
                }
            }
            return RedirectToAction("Login");
        }

    }
}