namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryIdToItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItems", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.FoodItems", "CategoryId");
            AddForeignKey("dbo.FoodItems", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodItems", "CategoryId", "dbo.Categories");
            DropIndex("dbo.FoodItems", new[] { "CategoryId" });
            DropColumn("dbo.FoodItems", "CategoryId");
        }
    }
}
