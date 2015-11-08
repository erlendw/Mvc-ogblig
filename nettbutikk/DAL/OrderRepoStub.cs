﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using System.Web.Mvc;
using System.Diagnostics;


namespace nettButikkpls.DAL
{
    public class OrderRepoStub : IOrderRepo
    {
        public List<Order> allOrders()
        {
            List<Order> olist = new List<Order>();
            Order o = new Order()
            {
                orderId = 1,
                customerId = 1,
                timestamp = "20:20:20",
                sumtotal = 69,
            };
            olist.Add(o);
            olist.Add(o);
            olist.Add(o);
            return olist;
        }
        public void addToCart(int productid, int quantity)
        {
            List<int> p = new List<int>();
            for (int i = 0; i < quantity; i++)
                p.Add(productid);
            Cart c = new Cart()
            {
                customerid = 1,
                productids = p,
            };
        }
        public bool addOrderList(int orderid)
        {
            if (orderid < 0)
                return false;
            else
                return true;
        }
        public int saveOrder(float price, int customerid)
        {
            return customerid;
        }
        public List<Order> ListAllOrders()
        {
            List<Order> o = new List<Order>();
            o = allOrders();
            return o;

        }
        public bool DeleteOrder(int orderId)
        {
            if (orderId < 0)
                return false;
            else
                return true;

        }
        public List<OrderList> AllOrderLists()
        {
            throw new NotImplementedException();
        }
        public int TotalPrice(List<int> pid)
        {
            return 69;
        }
        public Product FindProduct(int productid)
        {
            Product p = new Product()
            {
                productid = productid,
                productname = "Te",
                price = 69,
                category = "Te",
                description = "Dette er te",
            };
            return p;
        }

        public Cart FormatCart(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}