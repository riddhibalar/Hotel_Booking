namespace ProjectHotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "Price", c => c.Int(nullable: false));
        }
    }
}
