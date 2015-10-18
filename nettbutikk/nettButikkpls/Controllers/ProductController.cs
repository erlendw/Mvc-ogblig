using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;

namespace nettButikkpls.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult ListProducts()
        {
            var db = new DbProduct();
            List<Product> allProducts = db.allProducts();
            return View(allProducts);
        }

        public ActionResult RegProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegProduct(Product inProduct)
        {
            var db = new DbProduct();
            bool OK = db.saveProduct(inProduct);
            if(OK)
            {
                return RedirectToAction("ListProducts");
            }
            return View();
        }
    }
}