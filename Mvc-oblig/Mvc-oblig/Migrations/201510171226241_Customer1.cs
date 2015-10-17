namespace Mvc_oblig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customer1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Customers", name: "PostalArea_ZipCode", newName: "ZipCode");
            RenameIndex(table: "dbo.Customers", name: "IX_PostalArea_ZipCode", newName: "IX_ZipCode");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Customers", name: "IX_ZipCode", newName: "IX_PostalArea_ZipCode");
            RenameColumn(table: "dbo.Customers", name: "ZipCode", newName: "PostalArea_ZipCode");
        }
    }
}
