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
            if (AccessOk())
            {
                return View();
            }
            return RedirectToAction("ListProducts");
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
            return Json(new { Message = string.Empty });
        }

        public Product FindProduct(int productid)
        {
            return _productBLL.FindProduct(productid);
        }
        public bool AccessOk()
        {
            Customer c = CurrentCustomer();
            return (c != null && c.isadmin);
        }
        public Customer CurrentCustomer()
        {
            HttpContext context = System.Web.HttpContext.Current;
            Customer c = new Customer();
            //If-statement for EnhetsTest
            if (context == null)
                c = (Customer)Session["CurrentUser"];
            else
                c = (Customer)context.Session["CurrentUser"];
            return c;
        }
    }
}