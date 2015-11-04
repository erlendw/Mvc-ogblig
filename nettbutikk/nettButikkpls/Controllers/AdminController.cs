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
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProduct(FormCollection inList, int productid)
        {
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
            var db = new OrderLogic();
            return db.FindProduct(productid);

        }
        public ActionResult ListCustomers()
        {
            var db = new CustomerLogic();
            IEnumerable<Customer> allProducts = db.allCustomers();
            return View(allProducts);
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
            var db = new OrderLogic();
            List<Order> orders = db.allOrders();
            return View(orders);
        }
        [HttpPost]
        public ActionResult DeleteOrder(int orderId)
        {
            var db = new OrderLogic();
            bool OK = db.DeleteOrder(orderId);
            if (OK)
            {
                return RedirectToAction("allOrders");
            }
            return RedirectToAction("allOrders");
        }
    }
}