namespace Mvc_oblig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "bruker_CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "bruker_CustomerId" });
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        bruker_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateIndex("dbo.Orders", "bruker_CustomerId");
            AddForeignKey("dbo.Orders", "bruker_CustomerId", "dbo.Customers", "CustomerId");
        }
    }
}
