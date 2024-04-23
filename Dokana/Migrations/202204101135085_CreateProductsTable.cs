namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProductsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitsInStore = c.Int(nullable: false),
                        AvailableToSale = c.Boolean(nullable: false),
                        Description = c.String(nullable: false),
                        ImageSrc = c.String(),
                        CategoryId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "UserId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Products");
        }
    }
}
