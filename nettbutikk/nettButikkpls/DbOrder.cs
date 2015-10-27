using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using System.Web.Mvc;

namespace nettButikkpls
{
    public class DbOrder
    {
        public void addToCart(int productid, int quantity)
        {
            HttpContext context = HttpContext.Current;

            if (context.Session["Cart"] == null)
            {
                Cart cart = new Cart();
                cart.productids[0].Add(productid);
                cart.productids[1].Add(quantity);
                context.Session["Cart"] = cart;
            }
            else
            {
                Cart cart = (Cart)context.Session["Cart"];
                cart.productids[0].Add(productid);
                cart.productids[1].Add(quantity);
                context.Session["Cart"] = cart;
            }
        }
        public bool saveOrer(Order inOrder)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    HttpContext context = HttpContext.Current;
                    Customer c = (Customer)context.Session["CurrentUser"];
                    String timeStamp = (DateTime.Now).ToString("yyyyMMddHHmmssffff");

                    var newOrderRow = new Orders();
                    newOrderRow.CustomerId = c.customerId;
                    newOrderRow.TimeStamp = timeStamp;
                    db.Orders.Add(newOrderRow);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception feil)
                {
                    return false;
                }
            }
        }
        public Product FindProduct(int productid)
        {
            using (var db = new NettbutikkContext())
            {
                List<Products> GetAllProducts = db.Products.ToList();
                Product c = new Product();
                for (int i = 0; i < GetAllProducts.Count; i++)
                {
                    if (GetAllProducts[i].ProductId == productid)
                    {
                        c.productid = productid;
                        c.productname = GetAllProducts[i].Productname;
                        c.price = GetAllProducts[i].Price;
                        c.category = GetAllProducts[i].Category;

                        return c;
                    }
                }
            }
            return null;
        }
    }
}