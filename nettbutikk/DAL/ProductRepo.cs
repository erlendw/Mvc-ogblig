using System;
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

                    //Start saving to Log
                    nettbutikkpls.Models.Log log = new nettbutikkpls.Models.Log();
                    log.ChangedTime = (DateTime.Now).ToString("yyyyMMddHHmmss");
                    log.EventType = "Create";
                    log.NewValue = newProductRow.ToString();
                    
                    if (HttpContext.Current.Session["CurrentCustomer"] != null)
                    {
                        Customer changedby = (Customer)HttpContext.Current.Session["CurrentCustomer"];
                        log.ChangedBy = changedby.firstname;
                    }
                    else
                    {
                        log.ChangedBy = "null";
                    }
                    SaveToLog(log.toString());
                    return true;
                }
                catch(Exception e)
                {
                    string message = "Exception: " + e + " catched at DeleteOrder()";
                    SaveToErrorLog(message);
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
        public void SaveToLog(string log)
        {
            string path = "Log.txt";
            var _Path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/"), path);
            if (!File.Exists(_Path))
            {
                string createText = log + Environment.NewLine;
                File.WriteAllText(_Path, createText);
            }
            else
            {
                string appendText = log + Environment.NewLine;
                File.AppendAllText(_Path, appendText);
            }
        }

        public void SaveToErrorLog(string log)
        {
            string path = "ErrorLog.txt";
            var _Path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/"), path);
            if (!File.Exists(_Path))
            {
                string createText = log + Environment.NewLine;
                File.WriteAllText(_Path, createText);
            }
            else
            {
                string appendText = log + Environment.NewLine;
                File.AppendAllText(_Path, appendText);
            }
        }
        public bool UpdateProduct(FormCollection inList, int productid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var product = db.Products.Single(b => (b.ProductId == productid));
                    Debug.Write(product.ProductId);
                    string originalvalue = product.ToString();
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
                    //Start saving to Log
                    nettbutikkpls.Models.Log log = new nettbutikkpls.Models.Log();
                    log.ChangedTime = (DateTime.Now).ToString("yyyyMMddHHmmss");
                    string newvalue = product.ToString();
                    log.ChangedTime = (DateTime.Now).ToString("yyyyMMddHHmmss");
                    log.EventType = "Update";
                    SaveToLog(log.toString());
                    return true;
                }
                catch (Exception e)
                {
                    string message = "Exception: " + e + " catched at DeleteOrder()";
                    SaveToErrorLog(message);
                    return false;
                }
            }
        }
        public Product FindProduct(int productid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    Product p = new Product();
                    var product = db.Products.Single(b => (b.ProductId == productid));
                    // var customer = db.Customers.Single(b => (b.CustomerId == customerid));

                    p.productid = productid;
                    p.productname = product.Productname;
                    p.price = product.Price;
                    p.category = product.Category;
                    p.description = product.Description;
                    return p;
                }
                catch (Exception e)
                {
                    return null;
                }

                /* List<Products> GetAllProducts = db.Products.ToList();
                 Product c = new Product();
                 for (int i = 0; i < GetAllProducts.Count; i++)
                 {
                     if (GetAllProducts[i].ProductId == productid)
                     {
                         c.productid = productid;
                         c.productname = GetAllProducts[i].Productname;
                         c.price = GetAllProducts[i].Price;
                         c.category = GetAllProducts[i].Category;
                         c.description = GetAllProducts[i].Description;

                         return c;
                     }
                 }*/
            }
        }
    }
}