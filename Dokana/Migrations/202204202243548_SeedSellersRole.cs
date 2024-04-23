namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedSellersRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0da420e9-12d8-4e12-a225-37ef0cdd5194', N'Sellers')");
        }
        
        public override void Down()
        {
        }
    }
}
