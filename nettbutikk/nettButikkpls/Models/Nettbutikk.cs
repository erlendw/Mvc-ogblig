
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Web;

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
    }
    public class NettbutikkContext : DbContext
    {
        
        public NettbutikkContext()
            : base("name=Nettbutikk")
        {
            Database.CreateIfNotExists();
        }
        public DbSet<Customers> Customers{ get; set; }
        public DbSet<PostalAreas> PostalAreas { get; set; }
        public DbSet<Products> Products { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}