namespace nettButikkpls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLists", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "String", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "String");
            DropColumn("dbo.OrderLists", "Quantity");
        }
    }
}
