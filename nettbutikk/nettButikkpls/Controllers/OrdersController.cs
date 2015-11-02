using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;
using System.Diagnostics;

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
            var db = new DbOrder();
            Cart cart;

            if (Session["Cart"] == null)
            {
                cart = new Cart();
                cart.productids = db.addToCart(productid, quantity);
                Session["Cart"] = cart;
            }
            else
            {
                cart = (Cart)Session["Cart"];
                cart.productids.AddRange(db.addToCart(productid, quantity));
                Session["Cart"] = cart;
            }
        }

        public ActionResult addOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            var db = new DbOrder();
            var cdb = new DbCustomer();
            Cart cart = (Cart)HttpContext.Session["Cart"];
            int sumTotal = SumTotal(cart.productids);
            int customerID = cdb.CurrentCustomer();
            Debug.Write("SumTotal" + sumTotal);
            int orderid = db.saveOrer(sumTotal, customerID);
            Debug.Print("Orderid: " + orderid);
            if (orderid!=0)
            {
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
            var db = new DbOrder();
            return db.FindProduct(productid);

        }   
    }
}