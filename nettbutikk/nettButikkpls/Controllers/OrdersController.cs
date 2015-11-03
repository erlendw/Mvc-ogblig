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

        public ActionResult allOrders()
        {
            var db = new OrderBLL();
            List<Order> allOrders = db.ListAllOrders();
            return View(allOrders);
        }

        public ActionResult addOrder()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            var db = new OrderBLL();
            var cdb = new CustomerBLL();
            Cart cart = (Cart)HttpContext.Session["Cart"];
            int sumTotal = SumTotal(cart.productids);
            int customerID = cdb.CurrentCustomerId();
            Debug.Write("SumTotal" + sumTotal);
            int orderid = db.saveOrder(sumTotal, customerID);
            Debug.Print("Orderid: " + orderid);
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
      
        public float TotalPrice(List<int> pid)
        {
            float price = 0;
            foreach (var i in pid)
            {
                price += FindProduct(i).price;
            }
            return price;
        }
        public int SumTotal(List<int> pid)
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
            var db = new OrderBLL();
            return db.FindProduct(productid);

        }  
        public ActionResult ListOrders()
        {
            var db = new OrderBLL();
            List<Order> orders = db.ListAllOrders();
            return View(orders);
        }
        [HttpPost]
        public ActionResult DeleteOrder(int orderId)
        {
            var db = new OrderBLL();
            bool OK = db.DeleteOrder(orderId);
            if (OK)
            {
                return RedirectToAction("allOrders");
            }
            return RedirectToAction("allOrders");
        }
    }
}