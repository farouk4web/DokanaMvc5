namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePaymentMethodFeeInOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PaymentMethodFee", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PaymentMethodFee", c => c.Int());
        }
    }
}
