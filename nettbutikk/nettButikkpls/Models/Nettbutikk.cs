using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace nettButikkpls.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Salt { get; set; }
        public string Zipcode { get; set; }
        public virtual PostalAreas Postalareas { get; set; }
    }
    public class PostalAreas
    {
        [Key]
        public string Zipcode { get; set; }
        public string Postalarea { get; set; }
        public virtual List<Customers> Customers { get; set; }
    }
    public class Products
    {  
        [Key]
        public int ProductId { get; set; }
        public string Productname { get; set; }
        public float Price { get; set; } 
        public string Category { get; set; }
        public string String { get; set; }
        public List<string> Picture { get; set; }

    }
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string TimeStamp { get; set; }
        public float SumTotal { get; set; }
    }
    public class OrderLists
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
    public class NettbutikkContext : DbContext
    {   
        public NettbutikkContext()
            : base("Nettbutikk")
        {
                Database.CreateIfNotExists();
        }
        public DbSet<Customers> Customers{ get; set; }
        public DbSet<PostalAreas> PostalAreas { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderLists> OrderLists { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<OrderLists>()
                .HasKey(c => new { c.OrderID, c.ProductID });
        }
    }
}