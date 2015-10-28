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
        public void addToCart(string Name, string Quantity)
        {

            Debug.Print(Name + " " + Quantity);

            var db = new DbOrder();

            db.addToCart(Int32.Parse(Name), Int32.Parse(Quantity));

            /*if (Session["Cart"] == null)
            {
                Cart cart = new Cart();
                cart.productids[0].Add(productid);
                cart.productids[1].Add(quantity);
                Session["Cart"] = cart;
            }
            else
            {
                Cart cart = (Cart)HttpContext.Session["Cart "];
                cart.productids[0].Add(productid);
                cart.productids[1].Add(quantity);
                Session["Cart"] = cart;
            }
            return RedirectToAction("Product", "ListProducts");*/
        }
        public ActionResult addOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            var db = new DbOrder();
            Cart cart = (Cart)HttpContext.Session["Cart"];
            float sumTotal = TotalPrice(cart.productids);
            int orderid = db.saveOrer(sumTotal);
            if (orderid!=0)
            {
                // metode(orderid); som legger inn i orderlist
                db.addOrderList(orderid);
                return RedirectToAction("Product", "ListProducts");
            }
            return RedirectToAction("Customer", "List");
            
        }

        public bool AddToOrderList(int orderid)
        {
            var db = new DbOrder();
            OrderList list = new OrderList();
            
            return false;
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
        public Product FindProduct(int productid)
        {
            var db = new DbOrder();
            return db.FindProduct(productid);
        }   
    }
}