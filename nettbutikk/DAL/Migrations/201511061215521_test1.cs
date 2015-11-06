namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "CustomerId");
            CreateIndex("dbo.OrderLists", "OrderID");
            CreateIndex("dbo.OrderLists", "ProductID");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.OrderLists", "OrderID", "dbo.Orders", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.OrderLists", "ProductID", "dbo.Products", "ProductId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLists", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderLists", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.OrderLists", new[] { "ProductID" });
            DropIndex("dbo.OrderLists", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
        }
    }
}
