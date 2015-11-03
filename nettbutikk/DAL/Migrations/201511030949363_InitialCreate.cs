namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Mail = c.String(),
                        Password = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Address = c.String(),
                        Salt = c.String(),
                        Zipcode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.PostalAreas", t => t.Zipcode)
                .Index(t => t.Zipcode);
            
            CreateTable(
                "dbo.PostalAreas",
                c => new
                    {
                        Zipcode = c.String(nullable: false, maxLength: 128),
                        Postalarea = c.String(),
                    })
                .PrimaryKey(t => t.Zipcode);
            
            CreateTable(
                "dbo.OrderLists",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        UnitPrice = c.Single(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID });
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        TimeStamp = c.String(),
                        SumTotal = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Productname = c.String(),
                        Price = c.Single(nullable: false),
                        Category = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Zipcode", "dbo.PostalAreas");
            DropIndex("dbo.Customers", new[] { "Zipcode" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLists");
            DropTable("dbo.PostalAreas");
            DropTable("dbo.Customers");
        }
    }
}
