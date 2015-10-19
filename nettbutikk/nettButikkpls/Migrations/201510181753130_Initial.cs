namespace nettButikkpls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Products1", newName: "Product");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Product", newName: "Products1");
        }
    }
}
