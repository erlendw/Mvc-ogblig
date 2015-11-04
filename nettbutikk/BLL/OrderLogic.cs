﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;
using nettButikkpls.DAL;
using System.Web.Mvc;
using System.Diagnostics;

namespace nettButikkpls.BLL
{
    public class OrderLogic : IOrderLogic
    {
        private IOrderRepo _repo;

        public OrderLogic()
        {
            _repo = new OrderRepo();
        }
        public OrderLogic(IOrderRepo stub)
        {
            _repo = stub;
        }
        public void addToCart(int productid, int quantity)
        {
            
            _repo.addToCart(productid, quantity);
        }
        public bool addOrderList(int orderid)
        {
            
            return _repo.addOrderList(orderid);
        }
        public int saveOrder(float price, int customerid)
        {
            
            return _repo.saveOrder(price, customerid);
        }
        public Product FindProduct(int productid)
        {
            
            return _repo.FindProduct(productid);
        }
        public List<Order> ListAllOrders()
        {
            
            return _repo.ListAllOrders();
        }
        public List<Order> allOrders()
        {
            
            return _repo.allOrders();
        }
        public bool DeleteOrder(int orderId)
        {
            
            return _repo.DeleteOrder(orderId);
        }
    }
}