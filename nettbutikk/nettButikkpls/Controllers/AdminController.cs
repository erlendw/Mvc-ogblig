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
        private ICustomerLogic _customerBLL;
        private IProductLogic _productBLL;
        private IOrderLogic _orderBLL;

        public AdminController()
        {
            _customerBLL = new CustomerLogic();
            _productBLL = new ProductLogic();
            _orderBLL = new OrderLogic();
        }

        public AdminController(ICustomerLogic stub) { _customerBLL = stub; }
        public AdminController(IProductLogic stub) { _productBLL = stub; }
        public AdminController(IOrderLogic stub) { _orderBLL = stub; }

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
                bool OK = _productBLL.UpdateProduct(inList, productid);
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
                IEnumerable<Product> allProducts = _productBLL.allProducts();
                return View(allProducts);
            }
            return RedirectToAction("Redirect");
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
            return _productBLL.FindProduct(productid);
        }
        public ActionResult ListCustomers()
        {
            if (AccessOk())
            {
                IEnumerable<Customer> allCustomers = _customerBLL.allCustomers();
                if (allCustomers != null)
                {
                    return View(allCustomers);
                }
                return RedirectToAction("AdminPanel");
            }
            return RedirectToAction("Redirect");
        }
        [HttpGet]
        public ActionResult EditCustomer(int? id)
        {
            if (AccessOk())
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
            return RedirectToAction("Redirect");
        }
        public Customer FindCustomer(int customerid)
        {
            return _customerBLL.FindCustomer(customerid);
        }
        [HttpPost]
        public ActionResult UpdateCustomer(FormCollection inList, int customerid)
        {
            if (AccessOk())
            {
                bool OK = _customerBLL.UpdateCustomer(inList, customerid);
                if (OK)
                {
                    return RedirectToAction("ListCustomers");
                }
                return RedirectToAction("AdminPanel");
            }
            return RedirectToAction("Redirect");
        }
        public ActionResult ListOrders()
        {
            /*if (AccessOk())
            {*/
                List<OrderList> orderlists = _orderBLL.AllOrderLists();
                return View(orderlists);
            /*}
            return RedirectToAction("Redirect");*/
        }
        [HttpGet]
        public ActionResult DeleteOrder(int orderId)
        {
            if (AccessOk())
            {
                bool OK = _orderBLL.DeleteOrder(orderId);
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
            return RedirectToAction("Login", "Customer");
        }
        public bool AccessOk()
        {
            Customer c = CurrentCustomer();
            return (c != null && c.isadmin);
        }
        public Customer CurrentCustomer()
        {
            HttpContext context = System.Web.HttpContext.Current;
            Customer c = new Customer();
            //If-statement for EnhetsTest
            if (context == null)
                c = (Customer)Session["CurrentUser"];
            else
                c = (Customer)context.Session["CurrentUser"];
            return c;
        }
    }
}