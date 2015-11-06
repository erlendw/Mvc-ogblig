using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace nettButikkpls.Models
{
    public class OrderList
    {    
        public int orderId { get; set; }
        public int productId { get; set; }
        public float unitPrice { get; set; }
        public List<int> quantity { get; set; }
        public Order order { get; set; }
        public List<Product> product { get; set; }
    }
}