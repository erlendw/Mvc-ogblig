namespace nettButikkpls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Description", c => c.String());
            DropColumn("dbo.Products", "String");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "String", c => c.String());
            DropColumn("dbo.Products", "Description");
        }
    }
}
