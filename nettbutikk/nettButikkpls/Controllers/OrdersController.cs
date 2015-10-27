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
        public ActionResult addToCart(int productid)
        {
            if (Session["Cart"] == null)
            {
                Cart cart = new Cart();
                cart.productids.Add(productid);
                Session["Cart"] = cart;
            }
            else
            {
                Cart cart = (Cart)HttpContext.Session["Cart"];
                cart.productids.Add(productid);
            }
            return RedirectToAction("Product", "ListProducts");
        }
        public ActionResult addOrder()
        {
            return View();
        }
    }
}