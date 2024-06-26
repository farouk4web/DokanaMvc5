namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeliveredToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsDelivered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsDelivered");
        }
    }
}
