﻿using System;
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
        public ActionResult ListProducts()
        {
            var db = new ProductBLL();
            IEnumerable<Product> allProducts = db.allProducts();
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
            Debug.Print(p.productname);
            return View(p);

            }
        }

        [HttpPost]
        public ActionResult RegProduct(Product inProduct)
        {
            var db = new ProductBLL();
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
            var db = new ProductBLL();
            bool success = db.SaveImagesToServer(innfiler);
            return Json(new { Message = string.Empty });
        }

        public Product FindProduct(int productid)
        {
            var db = new OrderBLL();
            return db.FindProduct(productid);

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
                Debug.Print(p.productname);
                return View(p);
            }
        }
    }
}