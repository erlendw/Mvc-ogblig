﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using System.Web.Mvc;
using System.Diagnostics;

namespace nettButikkpls
{
    public class DbOrder
    {
        HttpContext context = HttpContext.Current;
        public void addToCart(int productid, int quantity)
        {
            //Debug.Print("ProduID: " + productid + " Quantity: " + quantity);
            int customerid;
            //HttpContext context = HttpContext.Current;
          
            if (context.Session["Cart"] == null)
            {
                
                Cart cart = new Cart();
                
                if (context.Session["CurrentUser"] != null)
                {
                    Customers c = (Customers)context.Session["CurrentUser"];
                    customerid = c.CustomerId;
                    cart.customerid = customerid;
                }
               // Debug.Print("Cart.CustomerID: " + cart.customerid);
                context.Session["Cart"] = cart;
                for (int i = 0; i <= quantity; i++)
                {
                    cart.productids.Add(productid); 
                }
                
                context.Session["Cart"] = cart;     
            } 
            else
            {
                Cart cart = (Cart)context.Session["Cart"];
                for (int i = 0; i <= quantity; i++)
                {
                    cart.productids.Add(productid);
                }
                context.Session["Cart"] = cart;
            }
        }
        public bool addOrderList(int orderid)
        {
            try
            {
                Cart cart = (Cart)context.Session["Cart"];
                //var g = cart.productids.GroupBy(i => i);
                //OrderLists list = new OrderLists();
                
                
                int listsize = cart.productids.Count();
                int distinct = (from x in cart.productids select x).Distinct().Count();
                //Debug.Print("Distinct teas: " + distinct + ", Number of teas in cart: " + listsize);
                int[] pids = new int[distinct];
                //int[] pidsdesc = new int[listsize];
                List<int> pidlistdesc = new List<int>();
                //pidlistdesc = cart.productids.OrderByDescending(p => p).ToList();
                pidlistdesc = cart.productids.Distinct().ToList();
                List<int> count = new List<int>();
                
                foreach (int p in pidlistdesc)
                {
                    int c = cart.productids.Count(x => x == p);
                    count.Add(c);
                }
               
                if (listsize > distinct)
                {
                    for (int i = 0; i < listsize; i++)
                    {
                        Debug.Print("pid " + pidlistdesc[i]);
                        Debug.Print("Count " + count[i]);
                        OrderLists list = new OrderLists();
                        list.OrderID = orderid;
                        list.ProductID = pidlistdesc[i];
                        list.Quantity = count[i];
                        list.UnitPrice = FindProduct(pidlistdesc[i]).price;
                    }
                }
                else
                {
                    foreach (int p in cart.productids)
                    {
                        OrderLists list = new OrderLists();
                        //Debug.Print("ProductIDS i Carten: " + p);
                        list.OrderID = orderid;
                        list.ProductID = p;
                        list.UnitPrice = FindProduct(p).price;
                        list.Quantity = 1;
                    }
                }
                
                context.Session["Cart"] = null;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public int saveOrer(float price, int customerid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    Debug.Write("KOMMER TIL TRY");
                    String timeStamp = (DateTime.Now).ToString("yyyyMMddHHmmssffff");
                   // Debug.Write("CustomerID " + c.customerId);
                    var newOrderRow = new Orders();
                    newOrderRow.CustomerId = customerid;
                    newOrderRow.TimeStamp = timeStamp;
                    newOrderRow.SumTotal = price;
                    db.Orders.Add(newOrderRow);
                    db.SaveChanges();

                    List<Orders> GetAllOrders = db.Orders.ToList();
                    return GetAllOrders.Count;
                }
                catch (Exception feil)
                {
                    return 0;
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
                        c.description = GetAllProducts[i].Description;

                        return c;
                    }
                }
            }
            return null;
        }
    }
}