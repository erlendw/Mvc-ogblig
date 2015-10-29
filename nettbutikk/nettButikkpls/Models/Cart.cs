using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;

namespace nettButikkpls.Models
{
    public class Cart
    {
        public int customerid { get; set; }
        public List<int> productids { get; set; }

        public string ToString()
        {
            string print = "CART: Customer ID: " + customerid;
            foreach (int p in productids)
            {
                print += " Product ID: " + p;
            }
            return print;
        }
    }
}