using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;
using System.Diagnostics;


namespace nettButikkpls.Controllers
{
    public class CartController : Controller
    {
        NettbutikkContext db = new NettbutikkContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public void Add(Products product)
        {
            if (Session["Cart"] == null)
            {
                Cart cart = new Cart();
                //Debug.WriteLine("Cart: " + cart);
                Debug.WriteLine("Produktet i add: " + product.Productname);
                cart.products.Add(product);
                Session["Cart"] = cart;
                Debug.WriteLine("Cart: Vare 1: "+cart.products[0].Productname);
            }
            else
            {
                Cart cart = (Cart)HttpContext.Session["Cart"];
                cart.products.Add(product);
                Debug.WriteLine("Cart: Vare 2: " + cart.products[1].Productname);

            }
            Debug.WriteLine(Session["Cart"].ToString());
        }

        [HttpPost]
        public void AddProductToCart(string Name)
        {
            Products p = FindProductByName(Name);
            Add(p);
            Debug.WriteLine(Name);

                
            
        }

        public Products FindProductByName(string Name)
        {
            List<Products> GetAllProducts = db.Products.ToList();
            for (int i = 0; i < GetAllProducts.Count; i++)
            {
                if (GetAllProducts[i].Productname == Name)
                {
                    Debug.WriteLine("PRODUKTET: "+GetAllProducts[i].Productname);
                    return GetAllProducts[i];
                }
            }
            return null;
        }

    }
}