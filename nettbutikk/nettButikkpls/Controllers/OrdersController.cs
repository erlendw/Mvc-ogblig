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
            //Debug.Print("ProduID: " + productid + "Quantity: " + quantity);
            //HttpContext context = HttpContext.Current;

            if (Session["Cart"] == null)
            {
                Cart cart = new Cart();
                List<int> pIds = new List<int>();
                //Debug.Print("Cart.CustomerID: " + cart.customerid);
                Session["Cart"] = cart;
                for (int i = 0; i < quantity; i++)
                {
                    pIds.Add(productid);
                }
                cart.productids = pIds;
                Session["Cart"] = cart;
                Debug.Print("Cart:" + cart.productids.ToString());

            }
            else
            {
                Cart cart = (Cart)Session["Cart"];
                List<int> pIds = new List<int>();
                for (int i = 0; i < quantity; i++)
                {
                    pIds.Add(productid);
                }
                cart.productids.AddRange(pIds);
                Session["Cart"] = cart;
                Debug.Print("Cart:" + cart.productids.ToString());

            }
        }

       /* public ActionResult allOrders()
        {
            var db = new OrderLogic();
            List<Order> allOrders = db.ListAllOrders();
            return View(allOrders);
        }*/

        public ActionResult addOrder()
        {
            Customer c = (Customer)HttpContext.Session["CurrentUser"];
            if (c == null)
            {
                //If-statement for EnhetsTesting
                if (Session["CurrentUser"] != null)
                    return View();
                return RedirectToAction("ListProducts", "Product");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            //If-statement for EnhetsTest
            if (HttpContext.Session["Cart"] == null)
                HttpContext.Session["Cart"] = (Cart)Session["Cart"];
            Cart cart = (Cart)HttpContext.Session["Cart"];
            var db = new OrderLogic();
            var cdb = new CustomerLogic();
            int sumTotal = TotalPrice(cart.productids);
            int customerID = cdb.CurrentCustomerId();
            //Debug.Write("SumTotal" + sumTotal);
            int orderid = db.saveOrder(sumTotal, customerID);
            //Debug.Print("Orderid: " + orderid);
            if (orderid!=0)
            {
                // metode(orderid); som legger inn i orderlist
                db.addOrderList(orderid);
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
            int price = 0;
            foreach (int p in pid)
            {
                price += (int)FindProduct(p).price;
            }
            return price;
        }
        public Product FindProduct(int productid)
        {
            var db = new ProductController();
            return db.FindProduct(productid);

        }
        public ActionResult ListOrders()
        {
            List<Order> orders = _orderBLL.ListAllOrders();
            return View(orders);
        }
    }
}