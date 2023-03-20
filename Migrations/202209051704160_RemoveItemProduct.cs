namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveItemProduct : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ItemProducttlbs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ItemProducttlbs",
                c => new
                    {
                        ItemProductId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemPrice = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        TotalCostofItems = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ItemProductId);
            
        }
    }
}
