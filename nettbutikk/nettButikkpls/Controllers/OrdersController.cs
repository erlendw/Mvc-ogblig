using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;
using System.Diagnostics;
using nettButikkpls.BLL;

namespace nettButikkpls.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderLogic _orderBLL;

        public OrdersController()
        {
            _orderBLL = new OrderLogic();
        }

        public OrdersController(IOrderLogic stub)
        {
            _orderBLL = stub;
        }

        [HttpPost]
        public void addToCart(string Productid, string Quantity)
        {
            Debug.Print("Productid " + Productid);
            Debug.Print("Quantity " + Quantity);
            int productid = Int32.Parse(Productid);
            int quantity = Int32.Parse(Quantity);
            var db = new OrderLogic();
            if (Session["Cart"] == null)
            {
                Cart cart = new Cart();
                List<int> pIds = new List<int>();
                List<Product> products = new List<Product>();
                Session["Cart"] = cart;
                for (int i = 0; i < quantity; i++)
                {
                    pIds.Add(productid);
                    products.Add(db.FindProduct(productid));
                }
                cart.productids = pIds;
                cart.products = products;
                Debug.Print("PRoductek " + cart.products[0].productname);
                Session["Cart"] = cart;
                Debug.Print("Cart:" + cart.productids.ToString());

            }
            else
            {
                Cart cart = (Cart)Session["Cart"];
                List<int> pIds = new List<int>();
                List<Product> products = new List<Product>();
                for (int i = 0; i < quantity; i++)
                {
                    pIds.Add(productid);
                    products.Add(db.FindProduct(productid));
                }
                cart.productids.AddRange(pIds);
                cart.products.AddRange(products);
                Session["Cart"] = cart;
                Debug.Print("Cart:" + cart.productids.ToString());

            }
        }

        public ActionResult addOrder()
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            Cart cart = (Cart)HttpContext.Session["Cart"];
            if (c == null)
            {
                //If-statement for EnhetsTesting
                if (Session["CurrentUser"] != null)
                    return View();
                return RedirectToAction("ListProducts", "Product");
            }

            return View(cart);
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            //If-statement for EnhetsTest
            if (HttpContext.Session["Cart"] == null)
                HttpContext.Session["Cart"] = (Cart)Session["Cart"];
            Cart cart = (Cart)HttpContext.Session["Cart"];
            int sum = TotalPrice(cart.productids);
            int orderid = _orderBLL.saveOrder(sum, CurrentCustomerId());
            if (orderid!=0)
            {
                _orderBLL.addOrderList(orderid);
                return RedirectToAction( "OrderComplete", "Orders");
            }
            return RedirectToAction("Customer", "List");
        }
        public ActionResult OrderComplete()
        {
            return View();
        }
        public int TotalPrice(List<int> pid)
        {
            return _orderBLL.TotalPrice(pid);
        }
        public ActionResult ListOrders()
        {
            List<Order> orders = _orderBLL.ListAllOrders();
            return View(orders);
        }
        public int CurrentCustomerId()
        {
            HttpContext context = System.Web.HttpContext.Current;
            Customer c = new Customer();
            //If-statement for EnhetsTest
            if (context == null)
                c = (Customer)Session["CurrentUser"];
            else
                c = (Customer)context.Session["CurrentUser"];
            return c.customerId;
        }
    }
}