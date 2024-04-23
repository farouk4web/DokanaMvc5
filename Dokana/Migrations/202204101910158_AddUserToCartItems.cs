namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToCartItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CartItems", "UserId");
            AddForeignKey("dbo.CartItems", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CartItems", new[] { "UserId" });
            DropColumn("dbo.CartItems", "UserId");
        }
    }
}
