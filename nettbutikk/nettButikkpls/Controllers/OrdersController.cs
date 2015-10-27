using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;

namespace nettButikkpls.Controllers
{
    public class OrdersController : Controller
    {
        public ActionResult addToCart(int productid, int quantity)
        {
            var db = new DbOrder();
            db.addToCart(productid, quantity);
            return RedirectToAction("Shared", "Layout");


            /*if (Session["Cart"] == null)
            {
                Cart cart = new Cart();
                cart.productids[0].Add(productid);
                cart.productids[1].Add(quantity);
                Session["Cart"] = cart;
            }
            else
            {
                Cart cart = (Cart)HttpContext.Session["Cart"];
                cart.productids[0].Add(productid);
                cart.productids[1].Add(quantity);
                Session["Cart"] = cart;
            }
            return RedirectToAction("Product", "ListProducts");*/
        }
        public ActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddOrder()
        {
            Order order = new Order();
            return null;
            //fix
        }
        public Product FindProduct(int productid)
        {
            var db = new DbOrder();
            return db.FindProduct(productid);
        }
        public int TotalPrice()
        {
            return 0; //fix
        }
    }
}