using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nettButikkpls.Models;

namespace nettButikkpls.Models
{
    public class Cart
    {
        public string timestamp;
        public List<Products> products = new List<Products>();
    }
}