using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;

namespace nettButikkpls
{
    public class DbOrder
    {
        public IEnumerable<Order> allOrders()
        {
            using (var db = new NettbutikkContext())
            {
                var allProducts = db.Products
                .ToList()
                .Select(p => new Product
                {
                    productname = p.Productname,
                    price = p.Price,
                    category = p.Category
                }).ToList();
                return allOrders();
            }
        }

        
        public bool saveOrder(Order inOrder)
        {
            using (var db = new NettbutikkContext())
            {
                try
                {
                    var newOrderRow = new Orders();
                    
                    newOrderRow.OrderId = inOrder.id;
                    

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
    }
}