namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePaymentMethodsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods");
            DropIndex("dbo.Orders", new[] { "PaymentMethodId" });
            DropTable("dbo.PaymentMethods");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 500),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImgSrc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Orders", "PaymentMethodId");
            AddForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods", "Id");
        }
    }
}
