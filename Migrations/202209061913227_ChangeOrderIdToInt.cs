namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderIdToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderItems", "OrderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderItems", "OrderId", c => c.String());
        }
    }
}
