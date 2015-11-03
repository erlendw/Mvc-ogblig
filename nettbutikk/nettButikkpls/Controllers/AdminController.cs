using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.BLL;
using nettButikkpls.Models;

namespace nettButikkpls.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminPanel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateProduct(FormCollection inList, int productid)
        {
            var db = new ProductBLL();
            bool OK = db.UpdateProduct(inList, productid);
            if (OK)
            {
                return RedirectToAction("AdminPanel");
            }
            return RedirectToAction("AdminPanel");
        }
        public ActionResult ListProductsAdmin()
        {
            var db = new ProductBLL();
            IEnumerable<Product> allProducts = db.allProducts();
            return View(allProducts);
        }
        [HttpGet]
        public ActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("EditProduct");
            }
            else
            {
                Product p = FindProduct((int)id);
                return View(p);
            }
        }
        public Product FindProduct(int productid)
        {
            var db = new OrderBLL();
            return db.FindProduct(productid);

        }
    }
}