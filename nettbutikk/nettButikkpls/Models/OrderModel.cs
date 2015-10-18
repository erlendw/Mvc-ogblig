namespace nettButikkpls.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class OrderModel : DbContext
    {
        // Your context has been configured to use a 'OrderModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Mvc_oblig.Models.OrderModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'OrderModel' 
        // connection string in the application configuration file.
        public OrderModel()
            : base("name=OrderModel")
        {
            Database.CreateIfNotExists();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Order> Order { get; set; }
    }

    public class Order
    {
        [Key]
        public int id { get; set; }
        /*public int CardNumber { get; set; }
        public int CVC { get; set; }
        public int ExpiryDate { get; set; }
        public string HolderName { get; set; }*/
        public Customer bruker { get; set; }

    }
}