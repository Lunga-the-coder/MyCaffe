namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToFoodItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItems", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItems", "ImageUrl");
        }
    }
}
