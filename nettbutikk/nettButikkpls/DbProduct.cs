using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;

namespace nettButikkpls
{
    public class DbProduct
    {
        public List<Product> allProducts()
        {
            using (var db = new NettbutikkContext())
            {
                List<Product> allProducts = db.Products.Select(p => new Product
                {
                    productid = p.ProductId,
                    productname = p.Productname,
                    price = p.Price,
                    category = p.Category
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
    }
}