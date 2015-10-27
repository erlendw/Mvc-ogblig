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
        public void addToCart(int productid)
        {
            HttpContext context = HttpContext.Current;

            if (context.Session["Cart"] == null)
            {
                Cart cart = new Cart();
                cart.productids.Add(productid);
                context.Session["Cart"] = cart;
            }
            else
            {
                Cart cart = (Cart)context.Session["Cart"];
                cart.productids.Add(productid);
            }
        }         
    }
}