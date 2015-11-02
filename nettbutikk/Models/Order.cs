using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace nettButikkpls.Models
{
    public class Order
    {
        public int orderId { get; set; }
        public int customerId { get; set; }
        public string timestamp { get; set; }
        public float sumtotal { get; set; }
    }
}