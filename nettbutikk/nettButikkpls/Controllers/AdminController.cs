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
        // GET: Admin
        public ActionResult AdminPanel()
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            if (c == null)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProduct(FormCollection inList, int productid)
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            if (c == null)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            var db = new ProductLogic();
            bool OK = db.UpdateProduct(inList, productid);
            if (OK)
            {
                return RedirectToAction("AdminPanel");
            }
            return RedirectToAction("AdminPanel");
        }
        public ActionResult ListProducts()
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            if (c == null)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            var db = new ProductLogic();
            IEnumerable<Product> allProducts = db.allProducts();
            return View(allProducts);
        }

        [HttpGet]
        public ActionResult EditProduct(int? id)
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
        public Product FindProduct(int productid)
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            if (c == null)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            var db = new OrderLogic();
            return db.FindProduct(productid);

        }
        public ActionResult ListCustomers()
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            if (c == null)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            var db = new CustomerLogic();
            IEnumerable<Customer> allCustomers = db.allCustomers();
            if (allCustomers != null)
            {
                return View(allCustomers);
            }
            return RedirectToAction("Redirect");
        }
        //HER ER JEG USIKKER PÅ HVA SOM HAR BLITT ENDRET, KOMMENTERER UT FOR Å TESTE LØSNINGEN! /Trym
        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
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
        public Customer FindCustomer(int customerid)
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            if (c == null)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            var db = new CustomerLogic();
            return db.FindCustomer(customerid);
        }
        [HttpPost]
        public ActionResult UpdateCustomer(FormCollection inList, int customerid)
        {
            var db = new CustomerLogic();
            Debug.Write("CUSTOMER " + customerid);
            bool OK = db.UpdateCustomer(inList, customerid);
            if (OK)
            {
                return RedirectToAction("AdminPanel");
            }
            return RedirectToAction("AdminPAnel");
        }
        public ActionResult ListOrders()
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            if (c == null)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            var db = new OrderLogic();
            List<Order> orders = db.allOrders();
            return View(orders);
        }
        [HttpGet]
        public ActionResult DeleteOrder(int orderId)
        {
            Debug.Print(orderId.ToString());

            var db = new OrderLogic();
            bool OK = db.DeleteOrder(orderId);
            if (OK)
            {
                return RedirectToAction("ListOrders");
            }
            return RedirectToAction("ListOrders");
        }
        public ActionResult Redirect()
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            if (c == null)
            {
                return RedirectToAction("ListProducts", "Product");
            }
            Debug.Write("Du har ikke tilgang");
            return RedirectToAction("ListProducts", "Product");
        }
    }
}