﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using System.IO;
using System.Web.Mvc;
using System.Diagnostics;
namespace nettButikkpls.DAL
{
    public class ProductRepo : IProductRepo
    {
        public IEnumerable<Product> allProducts()
        {
            using (var db = new NettbutikkContext())
            {
                var allProducts = db.Products
                .ToList()
                .Select(p=>new Product
                {
                    productid = p.ProductId,
                    productname = p.Productname,
                    price = p.Price,
                    category = p.Category,
                    description = p.Description
                }).ToList();
            return allProducts;
            }
        }
        public bool saveProduct (Product inProduct)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var newProductRow = new Products();
                    newProductRow.ProductId = inProduct.productid;
                    newProductRow.Productname = inProduct.productname;
                    newProductRow.Description = inProduct.description;
                    newProductRow.Price = inProduct.price;
                    newProductRow.Category = inProduct.category;
               
                    db.Products.Add(newProductRow);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception feil)
                {
                    return false;
                }
            }
        }
        public bool SaveImagesToServer(HttpFileCollectionBase innFiler)
        {
            foreach (string FileName in innFiler)
            {
                HttpPostedFileBase file = innFiler[FileName];

                var _FileName = Path.GetFileName(file.FileName);
                var _Path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Images"), _FileName);

                file.SaveAs(_Path);
                return true;
            }
            return true;
        }
        public bool UpdateProduct(FormCollection inList, int productid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var product = db.Products.Single(b => (b.ProductId == productid));

                    Debug.Write(product.ProductId);
                    if (!(String.IsNullOrEmpty(inList["Name"])))
                    {
                        product.Productname = inList["Name"];
                    }
                    if (!(String.IsNullOrEmpty(inList["Price"])))
                    {
                        product.Price = Int32.Parse(inList["Price"]);
                    }
                    if (!(String.IsNullOrEmpty(inList["Description"])))
                    {
                        product.Description = inList["Description"];
                    }
                    if (!(String.IsNullOrEmpty(inList["Category"])))
                    {
                        product.Category = inList["Category"];
                    }
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}