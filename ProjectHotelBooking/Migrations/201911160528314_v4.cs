namespace ProjectHotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Count", c => c.Int(nullable: false));
            AddColumn("dbo.Rooms", "ImagrUrl", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Image", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "ImagrUrl");
            DropColumn("dbo.Rooms", "Count");
        }
    }
}
