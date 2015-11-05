using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.BLL;
using nettButikkpls.Models;
using System.Diagnostics;

namespace nettButikkpls.Controllers
{
    public class AdminController : Controller
    {
        //mulig  endring her, hente current user, og sjekke om admin
        public ActionResult AdminPanel()
        {
            if (AccessOk())
            {
                return View();
            }
            return RedirectToAction("redirect");
        }
        [HttpPost]
        public ActionResult UpdateProduct(FormCollection inList, int productid)
        {
            if (AccessOk())
            {
                var db = new ProductLogic();
                bool OK = db.UpdateProduct(inList, productid);
                if (OK)
                {
                    //Melding her ; p
                    return RedirectToAction("AdminPanel");
                }
                //Melding her ; P
                return RedirectToAction("ListProducts");
            }
            return RedirectToAction("Redirect");
        }
        public ActionResult ListProducts()
        {
            if (AccessOk())
            { 
                var db = new ProductLogic();
                IEnumerable<Product> allProducts = db.allProducts();
                return View(allProducts);
            }
            return RedirectToAction("Redirect)");
        }

        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            if (AccessOk())
            {
                if (id == null)
                {
                    return RedirectToAction("EditProduct");
                }
                else
                {
                    Product p = FindProduct((int)id);
                    return View(p);
                }
            }
            return RedirectToAction("Redirect");
        }
        public Product FindProduct(int productid)
        {
            var db = new ProductLogic();
            return db.FindProduct(productid);
        }
        public ActionResult ListCustomers()
        {
            if (AccessOk())
            {
                var db = new CustomerLogic();
                IEnumerable<Customer> allCustomers = db.allCustomers();
                if (allCustomers != null)
                {
                    return View(allCustomers);
                }
                return RedirectToAction("AdminPanel");
            }
            return RedirectToAction("Redirect");
        }
        //HER ER JEG USIKKER PÅ HVA SOM HAR BLITT ENDRET, KOMMENTERER UT FOR Å TESTE LØSNINGEN! /Trym
        [HttpGet]
        public ActionResult EditCustomer(int? id)
        {
            if (AccessOk()) {
                if (id == null)
                {
                    return RedirectToAction("EditCustomer");
                }
                else
                {
                    Customer p = FindCustomer((int)id);
                    return View(p);
                }
            }
            return RedirectToAction("Redirect");
        }
        public Customer FindCustomer(int customerid)
        {
            var db = new CustomerLogic();
            return db.FindCustomer(customerid);
        }
        [HttpPost]
        public ActionResult UpdateCustomer(FormCollection inList, int customerid)
        {
            if (AccessOk())
            {
                var db = new CustomerLogic();
                Debug.Write("CUSTOMER " + customerid);
                bool OK = db.UpdateCustomer(inList, customerid);
                if (OK)
                {
                    return RedirectToAction("ListCustomers");
                }
                return RedirectToAction("AdminPAnel");
            }
            return RedirectToAction("Redirect");
        }
        public ActionResult ListOrders()
        {
            if (AccessOk())
            {
                var db = new OrderLogic();
                List<OrderList> orderlists = db.AllOrderLists();
                return View(orderlists);
            }
            return RedirectToAction("Redirect");
        }
        [HttpGet]
        public ActionResult DeleteOrder(int orderId)
        {
            if (AccessOk())
            {
                var db = new OrderLogic();
                bool OK = db.DeleteOrder(orderId);
                if (OK)
                {
                    return RedirectToAction("ListOrders");
                }
                return RedirectToAction("ListOrders");
            }
            return RedirectToAction("Redirect");
        }
        public ActionResult Redirect()
        {
            Debug.Write("Du har ikke tilgang");
            return RedirectToAction("Login", "Customer");
        }
        public bool AccessOk()
        {
            var db = new CustomerLogic();
            Customer c = db.CurrentCustomer();
            return (c != null && c.isadmin);
        }
    }
}