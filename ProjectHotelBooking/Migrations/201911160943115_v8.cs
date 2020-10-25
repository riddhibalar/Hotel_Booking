namespace ProjectHotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        contactno = c.Int(nullable: false),
                        email = c.String(nullable: false),
                        checkin = c.String(nullable: false),
                        checkout = c.String(nullable: false),
                        number_of_rooms = c.Int(nullable: false),
                        RoomsId = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Rooms", t => t.RoomsId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.RoomsId)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserID", "dbo.Users");
            DropForeignKey("dbo.Orders", "RoomsId", "dbo.Rooms");
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.Orders", new[] { "RoomsId" });
            DropTable("dbo.Orders");
        }
    }
}
