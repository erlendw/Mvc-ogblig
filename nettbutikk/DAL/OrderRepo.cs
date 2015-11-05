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
                    var prod = new ProductRepo();

                    foreach (int p in pidlistdesc)
                    {
                        int c = cart.productids.Count(x => x == p);
                        count.Add(c);
                    }

                    for (int i = 0; i < count.Count; i++)
                    {
                        OrderLists list = new OrderLists();
                        Products product = db.Products.Single(p=> (p.ProductId == pidlistdesc[i]));
                        list.OrderID = orderid;
                        list.Orders = FindOrder(orderid);
                        list.ProductID = pidlistdesc[i];
                        list.Quantity = count[i];
                        list.UnitPrice = product.Price;
                        list.Products = product;
                        db.OrderLists.Add(list);
                    }
                    db.SaveChanges();
                    context.Session["Cart"] = null;
                    //Start save to Log
                    nettbutikkpls.Models.Log log = new nettbutikkpls.Models.Log();
                    log.ChangedTime = (DateTime.Now).ToString("yyyyMMddHHmmss");
                    log.EventType = "Create";
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
                catch (Exception e)
                {
                    string message = "Exception: " + e + " catched at DeleteOrder()";
                    SaveToErrorLog(message);
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
                    String timeStamp = (DateTime.Now).ToString("yyyyMMddHHmmss");
                    // Debug.Write("CustomerID " + c.customerId);
                    var newOrderRow = new Orders();
                    newOrderRow.CustomerId = customerid;
                    newOrderRow.TimeStamp = timeStamp;
                    newOrderRow.SumTotal = price;
                    db.Orders.Add(newOrderRow);
                    db.SaveChanges();
                    //Start save to Log
                    nettbutikkpls.Models.Log log = new nettbutikkpls.Models.Log();
                    log.ChangedTime = timeStamp;
                    log.EventType = "Create";
                    log.NewValue = newOrderRow.ToString(); ;
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
                    List<Orders> GetAllOrders = db.Orders.ToList();
                    return GetAllOrders.Count;
                }
                catch (Exception e)
                {
                    string message = "Exception: " + e + " catched at DeleteOrder()";
                    SaveToErrorLog(message);
                    return 0;
                }
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

        public bool DeleteOrder(int orderId)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var order = db.Orders.Single(b => (b.OrderId == orderId));
                    string originalvalue = order.ToString();
                    db.Orders.Attach(order);
                    db.Orders.Remove(order);

                    var orderlists = db.OrderLists.Where(ol => ol.OrderID == orderId);

                    foreach (var ol in orderlists)
                    {
                        db.OrderLists.Attach(ol);
                        db.OrderLists.Remove(ol);
                    }
                    //Start save to Log
                    nettbutikkpls.Models.Log log = new nettbutikkpls.Models.Log();
                    log.ChangedTime = (DateTime.Now).ToString("yyyyMMddHHmmss");
                    log.EventType = "Delete";
                    log.OriginalValue = originalvalue;
                    log.NewValue = "null";
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
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    string message = "Exception: "+ e + " catched at DeleteOrder()";
                    SaveToErrorLog(message);
                    return false;
                }
            }
        }
        public Orders FindOrder(int orderid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var orders = db.Orders.Single(o => (o.OrderId == orderid));
                    return orders;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        public Order GetOrder(int orderid)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    Order o = new Order();
                    var order = db.Orders.Single(a => (a.OrderId == orderid));
                    o.orderId = orderid;
                    o.customerId = order.CustomerId;
                    o.sumtotal = order.SumTotal;
                    o.timestamp = order.TimeStamp;
                    return o;
                    
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        public List<OrderList> AllOrderLists()
        {
            using (var db = new NettbutikkContext())
            {
                var prod = new ProductRepo();
                List<OrderList> allOrderLists = db.OrderLists.Select(o => new OrderList
                {
                    orderId = o.OrderID,
                    productId = o.ProductID,
                    unitPrice = o.UnitPrice,
                    quantity = o.Quantity,
                    order = GetOrder(o.OrderID),
                    product = prod.FindProduct(o.ProductID)
                }).ToList();
                return allOrderLists;
            }
        }

        public List<OrderList> allOrderLists()
        {
            throw new NotImplementedException();
        }
    }
}