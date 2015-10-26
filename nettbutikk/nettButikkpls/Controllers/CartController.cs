using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;
using System.Diagnostics;

namespace nettButikkpls.Controllers
{
    public class CartController : Controller
    {
        NettbutikkContext db = new NettbutikkContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(Product product)
        {
            if (Session["Cart"] == null)
            {
                Cart cart = new Cart();
                cart.products.Add(product);
                Session["Cart"] = cart;
            }
            else
            {
                Cart cart = (Cart)HttpContext.Session["Cart"];
                cart.products.Add(product);
            }
            
            return RedirectToAction("ListProducts");
        }

        [HttpPost]
        public void SubmitSubscription(string Name)
        {
            Debug.WriteLine(Name);
        }

    }
}