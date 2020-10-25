namespace ProjectHotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Image", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "ImageURL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "ImageURL", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "Image");
        }
    }
}
