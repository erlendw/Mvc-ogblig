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
        private ICustomerLogic _customerBLL;

        public CustomerController()
        {
            _customerBLL = new CustomerLogic();
        }

        public CustomerController(ICustomerLogic stub)
        {
            _customerBLL = stub;
        }
        public ActionResult List()
        {
            
            List<Customer> allCustomers = _customerBLL.allCustomers();
            return View(allCustomers);
        }
        public ActionResult Reg()
        {
            return View();
        }
        /*public ActionResult CurrentCustomer()
        {
            
            return View(db.CurrentCustomerObj());
        }*/

        [HttpPost]
        public ActionResult Reg(Customer inCustomer)
        {
            
            bool OK = _customerBLL.saveCustomer(inCustomer);
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
            
            bool OK = _customerBLL.EditCustomer(inList);
            if(OK)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
        public Customer FindCustomerByEmail (string Email)
        {
            
            //List<Customer> GetAllCustomers = bmx.Customers.ToList();
            List<Customer> GetAllCustomers = _customerBLL.allCustomers();
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
            HttpContext context = System.Web.HttpContext.Current;
            if (context == null)
            {
                if (Session["CurrentUser"] != null)
                    return RedirectToAction("List");
                return View();
            }
                
            if (context.Session["CurrentUser"] != null)
            {
                return RedirectToAction("List");
            }
            return View(); //Implisitt else
        }
        [HttpPost]
        public ActionResult ValidateUser(FormCollection inList)
        {
            //Trenger feilmelding når brukervalidering feiler.
            
            bool loggedIn = _customerBLL.ValidateUser(inList);
            if (loggedIn)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            return RedirectToAction("Login");//Implisitt else
        }
        public int CurrentCustomerId()
        {
            return _customerBLL.CurrentCustomerId();
        }
    }

}