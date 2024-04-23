namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedShippingStaffRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO[dbo].[AspNetRoles]
                        ([Id], [Name]) VALUES(N'87c67596-2f60-4b7c-a979-7e66deb69aa4', N'ShippingStaff')");
        }

        public override void Down()
        {
        }
    }
}
