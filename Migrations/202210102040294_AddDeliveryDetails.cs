namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeliveryDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryDetails",
                c => new
                    {
                        DeliveryDetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        DeliveryCampus = c.Int(nullable: false),
                        DeliveryPrice = c.Double(nullable: false),
                        DeliveryBlock = c.String(nullable: false),
                        DeliveryInstructions = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryDetailId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeliveryDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.DeliveryDetails", new[] { "OrderId" });
            DropTable("dbo.DeliveryDetails");
        }
    }
}
