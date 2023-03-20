namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderItemTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemId = c.Int(nullable: false, identity: true),
                        OrderId = c.String(),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        UserId = c.String(),
                        FoodItem_FoodItemId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItemId)
                .ForeignKey("dbo.FoodItems", t => t.FoodItem_FoodItemId)
                .Index(t => t.FoodItem_FoodItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "FoodItem_FoodItemId", "dbo.FoodItems");
            DropIndex("dbo.OrderItems", new[] { "FoodItem_FoodItemId" });
            DropTable("dbo.OrderItems");
        }
    }
}
