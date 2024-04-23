namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveShoppingCartIdFromCartItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "ShoppingCartId", "dbo.ShoppingCarts");
            DropIndex("dbo.CartItems", new[] { "ShoppingCartId" });
            DropColumn("dbo.CartItems", "ShoppingCartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItems", "ShoppingCartId", c => c.Int(nullable: false));
            CreateIndex("dbo.CartItems", "ShoppingCartId");
            AddForeignKey("dbo.CartItems", "ShoppingCartId", "dbo.ShoppingCarts", "Id", cascadeDelete: true);
        }
    }
}
