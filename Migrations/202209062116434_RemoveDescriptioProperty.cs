namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDescriptioProperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FoodItems", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodItems", "Description", c => c.String(nullable: false));
        }
    }
}
