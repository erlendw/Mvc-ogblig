using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;
using nettButikkpls.BLL;
using System.Diagnostics;
using System.IO;

namespace nettButikkpls.Controllers
{
    public class ProductController : Controller
    {
        private IProductLogic _productBLL;

        public ProductController()
        {
            _productBLL = new ProductLogic();
        }

        public ProductController(IProductLogic stub)
        {
            _productBLL = stub;
        }
        public ActionResult ListProducts()
        {
            
            IEnumerable<Product> allProducts = _productBLL.allProducts();
            return View(allProducts);
        }

        public ActionResult RegProduct()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowProduct(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("ListProducts");
            }
            else
            { 

            Product p = FindProduct( (int) id );
            return View(p);
                }
        }

        [HttpPost]
        public ActionResult RegProduct(Product inProduct)
        {
            
            bool OK = _productBLL.saveProduct(inProduct);
            if(OK)
            {
                return RedirectToAction("ListProducts");
            }
            return View();
        }

        public ActionResult SaveImagesToServer()
        {
            HttpFileCollectionBase innfiler = Request.Files;
            
            bool success = _productBLL.SaveImagesToServer(innfiler);
            Debug.Print("success " + success);
            return Json(new { Message = string.Empty });
        }

        public Product FindProduct(int productid)
        {
            return _productBLL.FindProduct(productid);
        }
    }
}