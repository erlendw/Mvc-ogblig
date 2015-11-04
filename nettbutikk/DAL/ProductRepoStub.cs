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
    public class ProductRepoStub : IProductRepo
    {
        public IEnumerable<Product> allProducts()
        {
            List<Product> plist = new List<Product>();
            Product p = new Product()
            {
                productid = 1,
                productname = "Te",
                price = 69,
                category = "Te",
                description = "Dette er te",
            };
            plist.Add(p);
            plist.Add(p);
            plist.Add(p);
            IEnumerable<Product> enp = plist;
            return enp;
        }
        public bool saveProduct (Product inProduct)
        {
            if (inProduct != null)
                return true;
            else
                return false;   
        }
        public bool SaveImagesToServer(HttpFileCollectionBase innFiler)
        {
            if (innFiler != null)
                return true;
            else
                return false;
        }
        public bool UpdateProduct(FormCollection inList, int productid)
        {
            if (inList.Count < 1)
                return false;
            else
                return true;
        }
        public Product FindProduct(int productid)
        {
            Product p = new Product()
            {
                productid = productid,
                productname = "Te",
                price = 69,
                category = "Te",
                description = "Dette er te",
            };
            return p;
        }
    }
}