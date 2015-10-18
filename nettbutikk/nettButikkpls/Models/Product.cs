using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace nettButikkpls.Models
{
    public class Product
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public float price { get; set; }
        public string category { get; set; }
    }
}