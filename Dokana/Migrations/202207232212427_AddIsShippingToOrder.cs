namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsShippingToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsShipping", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsShipping");
        }
    }
}
