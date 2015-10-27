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
    }
}