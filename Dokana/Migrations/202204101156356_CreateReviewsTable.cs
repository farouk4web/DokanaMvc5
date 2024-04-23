namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateReviewsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StarsCount = c.Byte(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 500),
                        DateOfCreate = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "ProductId", "dbo.Products");
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "ProductId" });
            DropTable("dbo.Reviews");
        }
    }
}
