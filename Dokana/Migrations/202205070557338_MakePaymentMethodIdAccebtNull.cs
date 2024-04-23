namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePaymentMethodIdAccebtNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods");
            DropIndex("dbo.Orders", new[] { "PaymentMethodId" });
            AlterColumn("dbo.Orders", "PaymentMethodId", c => c.Int());
            CreateIndex("dbo.Orders", "PaymentMethodId");
            AddForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods");
            DropIndex("dbo.Orders", new[] { "PaymentMethodId" });
            AlterColumn("dbo.Orders", "PaymentMethodId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "PaymentMethodId");
            AddForeignKey("dbo.Orders", "PaymentMethodId", "dbo.PaymentMethods", "Id", cascadeDelete: true);
        }
    }
}
