using System;
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
        public List<int> addToCart(int productid, int quantity)
        {
            List<int> pIds = new List<int>();
            for (int i = 0; i < quantity; i++)
            {
                pIds.Add(productid);
            }
            return pIds;
           /* if (!cart.productids.Any())
            {
                cart.productids = pIds;
            }
            else
            {
                cart.productids.AddRange(pIds);
            }
            Debug.Print("Cart:" + cart.productids.ToString());
            return cart;*/
        }
        public bool addOrderList(int orderid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    Cart cart = (Cart)context.Session["Cart"];
                    List<int> pidlistdesc = new List<int>();
                    pidlistdesc = cart.productids.Distinct().ToList();
                    List<int> count = new List<int>();

                    foreach (int p in pidlistdesc)
                    {
                        int c = cart.productids.Count(x => x == p);
                        count.Add(c);
                    }

                    for (int i = 0; i < count.Count; i++)
                    {
                        Debug.Print("pid " + pidlistdesc[i]);
                        Debug.Print("Count " + count[i]);
                        OrderLists list = new OrderLists();
                        list.OrderID = orderid;
                        list.ProductID = pidlistdesc[i];
                        list.Quantity = count[i];
                        list.UnitPrice = FindProduct(pidlistdesc[i]).price;
                        db.OrderLists.Add(list);
                    }
                    db.SaveChanges();
                    context.Session["Cart"] = null;
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
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