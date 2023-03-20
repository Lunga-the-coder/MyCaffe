namespace MyCaffe.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUserTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[UserTypes]([Name]) VALUES ('Admin')");
            Sql("INSERT INTO [dbo].[UserTypes]([Name]) VALUES ('Student')");
        }
        
        public override void Down()
        {
        }
    }
}
