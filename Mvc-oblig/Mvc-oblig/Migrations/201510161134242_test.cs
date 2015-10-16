namespace Mvc_oblig.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Mail = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        LastName = c.String(),
                        Address = c.String(),
                        Salt = c.String(),
                        PostalArea_ZipCode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.PostalAreas", t => t.PostalArea_ZipCode)
                .Index(t => t.PostalArea_ZipCode);
            
            CreateTable(
                "dbo.PostalAreas",
                c => new
                    {
                        ZipCode = c.String(nullable: false, maxLength: 128),
                        PostalArea_ = c.String(),
                    })
                .PrimaryKey(t => t.ZipCode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "PostalArea_ZipCode", "dbo.PostalAreas");
            DropIndex("dbo.Customers", new[] { "PostalArea_ZipCode" });
            DropTable("dbo.PostalAreas");
            DropTable("dbo.Customers");
        }
    }
}
