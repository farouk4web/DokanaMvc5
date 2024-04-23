namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateOrdersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 60),
                        Phone = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Region = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Street = c.String(),
                        MoreAboutAddress = c.String(maxLength: 500),
                        ShippingAmount = c.Decimal(precision: 18, scale: 2),
                        PaymentMethodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodId, cascadeDelete: true)
                .Index(t => t.PaymentMethodId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods");
            DropIndex("dbo.Orders", new[] { "PaymentMethodId" });
            DropTable("dbo.Orders");
        }
    }
}
