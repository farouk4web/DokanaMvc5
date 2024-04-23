namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGrandTotalToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "GrandTotal", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "GrandTotal");
        }
    }
}
