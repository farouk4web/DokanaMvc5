namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedAdminsRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'cbd4ecb3-7a5d-409f-9002-bc77aeaea3f2', N'Admins')");
        }

        public override void Down()
        {
        }
    }
}
