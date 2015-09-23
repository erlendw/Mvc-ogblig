﻿using System;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public virtual PostalArea PostalArea { get; set; } //lazy loader

    }

    public class PostalArea
    {

        [Key]
        public string ZipCode { get; set; }
        public string PostalArea { get; set; }


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
