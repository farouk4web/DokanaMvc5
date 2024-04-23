namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class CreateSuperAdminsRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'db3f0fea-97c5-491d-bbee-645d721cac76', N'SuperAdmins')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ae5be91b-0d59-4412-9704-b9d33b28e609', N'db3f0fea-97c5-491d-bbee-645d721cac76')
                ");
        }

        public override void Down()
        {
        }
    }
}
