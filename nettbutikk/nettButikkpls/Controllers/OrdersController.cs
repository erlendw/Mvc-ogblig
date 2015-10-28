using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nettButikkpls.Models;
using System.Diagnostics;

namespace nettButikkpls.Controllers
{
    public class OrdersController : Controller
    {
        [HttpPost]
        public void addToCart(string Productid, string Quantity)
        {
            Debug.Print("Productid " + Productid);
            Debug.Print("Quantity " + Quantity);
            int productid = Int32.Parse(Productid);
            int quantity = Int32.Parse(Quantity);
            //Debug.Print("ProduID: " + productid + "Quantity: " + quantity);
            //HttpContext context = HttpContext.Current;

            if (Session["Cart"] == null)
            {
                Cart cart = new Cart();
                List<int> pIds = new List<int>();
                //Debug.Print("Cart.CustomerID: " + cart.customerid);
                Session["Cart"] = cart;
                for (int i = 0; i < quantity; i++)
                {
                    pIds.Add(productid);
                }
                cart.productids = pIds;
                Session["Cart"] = cart;
                //Debug.Print("Cart:" + cart.ToString());
            }
            else
            {
                Cart cart = (Cart)Session["Cart"];
                List<int> pIds = new List<int>();
                for (int i = 0; i < quantity; i++)
                {
                    pIds.Add(productid);
                }
                cart.productids.AddRange(pIds);
                Session["Cart"] = cart;
                //Debug.Print("Cart:" + cart.ToString());
            }
        }

        /*  Debug.Print(Name + " " + Quantity);
          var db = new DbOrder();
          db.addToCart(Int32.Parse(Name), Int32.Parse(Quantity));

          if (Session["Cart"] == null)
          {
              Cart cart = new Cart();
              cart.productids[0].Add(productid);  
              cart.productids[1].Add(quantity);
              Session["Cart"] = cart;
          }
          else
          {
              Cart cart = (Cart)HttpContext.Session["Cart "];
              cart.productids[0].Add(productid);
              cart.productids[1].Add(quantity);
              Session["Cart"] = cart;
          }
          return RedirectToAction("Product", "ListProducts");
    }*/
        public ActionResult addOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            var db = new DbOrder();
            Cart cart = (Cart)HttpContext.Session["Cart"];
            Debug.Print(cart.ToString());
            int sumTotal = SumTotal(cart.productids);
            Debug.Print("Total price: " + sumTotal);
            int orderid = db.saveOrer(sumTotal);
            Debug.Print("Orderid: " + orderid);
            if (orderid!=0)
            {
                // metode(orderid); som legger inn i orderlist
                
                db.addOrderList(orderid);
                return RedirectToAction("Product", "ListProducts");
            }
            return RedirectToAction("Customer", "List");
            
        }

        public bool AddToOrderList(int orderid)
        {
            var db = new DbOrder();
            OrderList list = new OrderList();
            
            return false;
        }
        public float TotalPrice(List<int> pid)
        {
            float price = 0;
            foreach (var i in pid)
            {
                price += FindProduct(i).price;
            }
            return price;
        }
        public int SumTotal(List<int> pid)
        {
            int price = 0;
            foreach (int p in pid)
            {
                price += (int)FindProduct(p).price;
            }
            return price;
        }
        public Product FindProduct(int productid)
        {
            var db = new DbOrder();
            return db.FindProduct(productid);
        }   
    }
}