namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShowInSliderToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShowInSlider", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ShowInSlider");
        }
    }
}
