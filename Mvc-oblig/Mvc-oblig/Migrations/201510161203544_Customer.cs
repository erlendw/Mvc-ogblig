namespace Mvc_oblig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Salt", c => c.String());
            AlterColumn("dbo.Customers", "Mail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Mail", c => c.String());
            DropColumn("dbo.Customers", "Salt");
        }
    }
}
