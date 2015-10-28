using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nettButikkpls.Models
{
    public class OrderList
    {    
        public int orderId { get; set; }
        public int productId { get; set; }
        public float unitPrice { get; set; }
        public int quanty { get; set; }
    }
}