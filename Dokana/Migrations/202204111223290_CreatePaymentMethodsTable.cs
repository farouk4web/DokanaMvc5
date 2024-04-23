namespace Dokana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePaymentMethodsTable : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentMethods");
        }
    }
}
