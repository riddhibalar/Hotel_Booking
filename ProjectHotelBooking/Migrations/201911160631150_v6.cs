namespace ProjectHotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "FileName", c => c.String(maxLength: 255));
            AddColumn("dbo.Rooms", "ContentType", c => c.String(maxLength: 100));
            AddColumn("dbo.Rooms", "Content", c => c.Binary());
            AddColumn("dbo.Rooms", "FileType", c => c.Int(nullable: false));
            DropColumn("dbo.Rooms", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "ImageUrl", c => c.String(nullable: false));
            DropColumn("dbo.Rooms", "FileType");
            DropColumn("dbo.Rooms", "Content");
            DropColumn("dbo.Rooms", "ContentType");
            DropColumn("dbo.Rooms", "FileName");
        }
    }
}
