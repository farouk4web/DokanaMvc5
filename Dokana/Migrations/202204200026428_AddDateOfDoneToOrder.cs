namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateOfDoneToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DateOfDone", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DateOfDone");
        }
    }
}
