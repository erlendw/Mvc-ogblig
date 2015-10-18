namespace nettButikkpls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ProductID_ProductId", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ProductID_ProductId" });
            DropColumn("dbo.Orders", "ProductID_ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ProductID_ProductId", c => c.Int());
            CreateIndex("dbo.Orders", "ProductID_ProductId");
            AddForeignKey("dbo.Orders", "ProductID_ProductId", "dbo.Products", "ProductId");
        }
    }
}
