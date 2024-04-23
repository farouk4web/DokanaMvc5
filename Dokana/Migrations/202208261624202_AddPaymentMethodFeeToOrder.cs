namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentMethodFeeToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PaymentMethodFee", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PaymentMethodFee");
        }
    }
}
