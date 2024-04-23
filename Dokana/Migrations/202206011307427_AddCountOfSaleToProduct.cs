namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountOfSaleToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CountOfSale", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CountOfSale");
        }
    }
}
