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
        public string SubmitSubscription(string Name, string Address)
        {
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Address))
                //TODO: Save the data in database
                return "Thank you " + Name + ". Record Saved.";
            else
                return "Please complete the form.";

        }

    }
}