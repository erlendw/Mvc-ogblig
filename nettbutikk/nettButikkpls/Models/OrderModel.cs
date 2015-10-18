namespace nettButikkpls.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    

    public class Order
    {
        
        public int id { get; set; }
        /*public int CardNumber { get; set; }
        public int CVC { get; set; }
        public int ExpiryDate { get; set; }
        public string HolderName { get; set; }*/
        public Customer bruker { get; set; }

    }
}