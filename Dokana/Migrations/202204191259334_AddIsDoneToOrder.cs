namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDoneToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsDone", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsDone");
        }
    }
}
