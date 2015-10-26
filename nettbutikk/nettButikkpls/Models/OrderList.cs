using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nettButikkpls.Models
{
    public class OrderList
    {    
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}