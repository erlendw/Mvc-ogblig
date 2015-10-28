namespace nettButikkpls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderLists", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderLists", "Quantity", c => c.Int(nullable: false));
        }
    }
}
