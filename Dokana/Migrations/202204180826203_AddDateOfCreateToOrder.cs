namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateOfCreateToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DateOfCreate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DateOfCreate");
        }
    }
}
