namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFoodItemForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "FoodItem_FoodItemId", "dbo.FoodItems");
            DropIndex("dbo.OrderItems", new[] { "FoodItem_FoodItemId" });
            RenameColumn(table: "dbo.OrderItems", name: "FoodItem_FoodItemId", newName: "FoodItemId");
            AlterColumn("dbo.OrderItems", "FoodItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderItems", "FoodItemId");
            AddForeignKey("dbo.OrderItems", "FoodItemId", "dbo.FoodItems", "FoodItemId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "FoodItemId", "dbo.FoodItems");
            DropIndex("dbo.OrderItems", new[] { "FoodItemId" });
            AlterColumn("dbo.OrderItems", "FoodItemId", c => c.Int());
            RenameColumn(table: "dbo.OrderItems", name: "FoodItemId", newName: "FoodItem_FoodItemId");
            CreateIndex("dbo.OrderItems", "FoodItem_FoodItemId");
            AddForeignKey("dbo.OrderItems", "FoodItem_FoodItemId", "dbo.FoodItems", "FoodItemId");
        }
    }
}
