using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;

namespace nettButikkpls.DAL
{
    public class OrderRepo : IOrderRepo
    {
        public List<Order> allOrders()
        {
            using (var db = new NettbutikkContext())
            {
                List<Order> allOrders = db.Orders.Select(o => new Order
                {
                    customerId = o.CustomerId,
                    orderId = o.OrderId,
                    timestamp = o.TimeStamp,
                    sumtotal = o.SumTotal
                }).ToList();
                return allOrders;
            }
        }
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
        public int saveOrder(float price, int customerid)
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
                try
                {
                    Product p = new Product();
                    var product = db.Products.Single(b=>(b.ProductId == productid));
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
        public List<Order> ListAllOrders()
        {
            using (var bmx = new NettbutikkContext())
            {
                var db = new CustomerRepo();
                int cId = db.CurrentCustomerId();
                List<Order> order = new List<Order>();
                IEnumerable<Orders> orders = bmx.Orders.Where(o => o.CustomerId == cId);

                foreach (var i in orders)
                {
                    Order or = new Order();
                    or.customerId = i.CustomerId;
                    or.orderId = i.OrderId;
                    or.sumtotal = i.SumTotal;
                    or.timestamp = i.TimeStamp;
                    order.Add(or);
                }
                return order;
            }
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
            Debug.Print("VICTORY");
        }
        public bool DeleteOrder(int orderId)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var order = db.Orders.Single(b => (b.OrderId == orderId));
                    db.Orders.Attach(order);
                    db.Orders.Remove(order);

                    var orderlists = db.OrderLists.Where(ol => ol.OrderID == orderId);

                    foreach (var ol in orderlists)
                    {
                        db.OrderLists.Attach(ol);
                        db.OrderLists.Remove(ol);
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