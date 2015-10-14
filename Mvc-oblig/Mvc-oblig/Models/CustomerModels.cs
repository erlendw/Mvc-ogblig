using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Mvc_oblig.Models
{
    public class Customer
    {        

        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Email")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Passwrod required")]
        public string Password { get; set; }

        public string LastName { get; set; }
        public string Address { get; set; }

        public string Salt { get; set; }

        public virtual PostalArea PostalArea { get; set; } //lazy loader

    }

    public class DbCustomer
    {

        [Key]
        public string Username;
        public string Password;

    }

    public class PostalArea
    {

        [Key]
        public string ZipCode { get; set; }
        public string PostalArea_ { get; set; }


    }

    public class CustomerContext : DbContext
    {

        public CustomerContext() : base("name=customer")
        {
            Database.CreateIfNotExists();

        }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<PostalArea> PostalArea { get; set; }


    }



}
