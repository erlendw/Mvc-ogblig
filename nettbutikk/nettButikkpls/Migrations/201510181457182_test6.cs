namespace nettButikkpls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test6 : DbMigration
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
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Timestamp = c.String(),
                        Mail_CustomerId = c.Int(),
                        ProductID_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Customers", t => t.Mail_CustomerId)
                .ForeignKey("dbo.Products", t => t.ProductID_ProductId)
                .Index(t => t.Mail_CustomerId)
                .Index(t => t.ProductID_ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Productname = c.String(),
                        Price = c.Single(nullable: false),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Products1",
                c => new
                    {
                        productid = c.Int(nullable: false, identity: true),
                        productname = c.String(),
                        price = c.Single(nullable: false),
                        category = c.String(),
                        Orders_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.productid)
                .ForeignKey("dbo.Orders", t => t.Orders_OrderId)
                .Index(t => t.Orders_OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products1", "Orders_OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ProductID_ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "Mail_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Zipcode", "dbo.PostalAreas");
            DropIndex("dbo.Products1", new[] { "Orders_OrderId" });
            DropIndex("dbo.Orders", new[] { "ProductID_ProductId" });
            DropIndex("dbo.Orders", new[] { "Mail_CustomerId" });
            DropIndex("dbo.Customers", new[] { "Zipcode" });
            DropTable("dbo.Products1");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.PostalAreas");
            DropTable("dbo.Customers");
        }
    }
}
