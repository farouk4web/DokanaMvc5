namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSignDateToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SignDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SignDate");
        }
    }
}
