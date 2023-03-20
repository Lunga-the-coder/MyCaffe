namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEstimatedAndActualDelivryToOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "EstimatedDelivery", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "ActualDelivery", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ActualDelivery");
            DropColumn("dbo.Orders", "EstimatedDelivery");
        }
    }
}
