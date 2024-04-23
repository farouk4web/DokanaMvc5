namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAdminUser : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [FullName], [ProfileImageSrc]) VALUES (N'ae5be91b-0d59-4412-9704-b9d33b28e609', N'farouk@dokana.com', 1, N'AFABYLAJstCPkBAOtb9WWMOJj0YowVI6rELNeD37ECpDZNQ2zSQBHETFIBwDdVIPIA==', N'808a8e1b-dac1-4552-8daf-ee49866b2189', NULL, 0, 0, NULL, 1, 0, N'farouk@dokana.com', N'Farouk Abdelhamid', N'/Content/Images/design/user.png')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ae5be91b-0d59-4412-9704-b9d33b28e609', N'cbd4ecb3-7a5d-409f-9002-bc77aeaea3f2')
                ");
        }
        
        public override void Down()
        {
        }
    }
}
