namespace ProjectHotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        RoomsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Rooms", t => t.RoomsId, cascadeDelete: true)
                .Index(t => t.RoomsId);
            
            DropColumn("dbo.Rooms", "FileName");
            DropColumn("dbo.Rooms", "ContentType");
            DropColumn("dbo.Rooms", "Content");
            DropColumn("dbo.Rooms", "FileType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "FileType", c => c.Int(nullable: false));
            AddColumn("dbo.Rooms", "Content", c => c.Binary());
            AddColumn("dbo.Rooms", "ContentType", c => c.String(maxLength: 100));
            AddColumn("dbo.Rooms", "FileName", c => c.String(maxLength: 255));
            DropForeignKey("dbo.Files", "RoomsId", "dbo.Rooms");
            DropIndex("dbo.Files", new[] { "RoomsId" });
            DropTable("dbo.Files");
        }
    }
}
