namespace ProjectHotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "ImageUrl", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "ImagrUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "ImagrUrl", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "ImageUrl");
        }
    }
}
