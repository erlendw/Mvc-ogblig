using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using nettButikkpls.DAL;
using System.Web.Mvc;
using System.Diagnostics;

namespace nettButikkpls.BLL
{
    public class OrderBLL
    {
        public void addToCart(int productid, int quantity)
        {
            var orderDal = new OrderDAL();
            orderDal.addToCart(productid, quantity);
        }
        public bool addOrderList(int orderid)
        {
            var orderDal = new OrderDAL();
            return orderDal.addOrderList(orderid);
        }
        public int saveOrder(float price, int customerid)
        {
            var orderDal = new OrderDAL();
            return orderDal.saveOrder(price, customerid);
        }
        public Product FindProduct(int productid)
        {
            var orderDal = new OrderDAL();
            return orderDal.FindProduct(productid);
        }
        public List<Order> ListAllOrders()
        {
            var orderDal = new OrderDAL();
            return orderDal.ListAllOrders();
        }
    }
}