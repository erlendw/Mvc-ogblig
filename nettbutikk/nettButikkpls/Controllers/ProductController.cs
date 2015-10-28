using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;
using System.Diagnostics;
using System.IO;

namespace nettButikkpls.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult ListProducts()
        {
            var db = new DbProduct();
            IEnumerable<Product> allProducts = db.allProducts();
            return View(allProducts);
        }

        [HttpPost]
        public ActionResult ShowProduct()
        {
            return RedirectToAction("Product", "ShowProduct");
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

        public ActionResult SaveImagesToServer()
        {
            HttpFileCollectionBase innfiler = Request.Files;
            var db = new DbProduct();
            bool erlend = db.SaveImagesToServer(innfiler);
            /*
            foreach (string FileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[FileName];

                var _FileName = Path.GetFileName(file.FileName);
                var _Path = Path.Combine(Server.MapPath("~/App_Data/Images"), _FileName);

                file.SaveAs(_Path);

                Debug.Print(file.FileName);
            }*/
            return Json(new { Message = string.Empty });
        }
    }
}