using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace nettButikkpls.Models
{
    public class Customer
    {

        public int customerId { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public string salt { get; set; }
        public string zipcode { get; set; }
        public string postalarea { get; set; }
    }
}
