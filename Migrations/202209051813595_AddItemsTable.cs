namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodItems",
                c => new
                    {
                        FoodItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.FoodItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FoodItems");
        }
    }
}
