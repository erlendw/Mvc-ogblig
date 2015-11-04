using System.Collections.Generic;
using nettButikkpls.Models;

namespace nettButikkpls.DAL
{
    public interface IOrderRepo
    {
        bool addOrderList(int orderid);
        void addToCart(int productid, int quantity);
        List<Order> allOrders();
        bool DeleteOrder(int orderId);
        Product FindProduct(int productid);
        List<Order> ListAllOrders();
        int saveOrder(float price, int customerid);
    }
}