namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeIsDoneAsRequiredInOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "IsDone", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "IsDone", c => c.Boolean());
        }
    }
}
