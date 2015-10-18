using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;

namespace nettButikkpls.Models
{
    public class Cart
    {

        public string timestamp { get; set; }
        public List<Product> products { get; set; }
    }
}