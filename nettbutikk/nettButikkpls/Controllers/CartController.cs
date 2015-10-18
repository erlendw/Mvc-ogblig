using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;

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
    }
}