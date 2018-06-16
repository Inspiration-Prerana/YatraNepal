namespace YatraNepal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminInfoes",
                c => new
                    {
                        AID = c.Int(nullable: false, identity: true),
                        AgentId = c.Guid(nullable: false),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        RegisteredDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AID);
            
            CreateTable(
                "dbo.AgentInfoes",
                c => new
                    {
                        AID = c.Int(nullable: false, identity: true),
                        AgentId = c.Guid(nullable: false),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        RegisteredDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LID = c.Int(nullable: false, identity: true),
                        strAddress = c.String(),
                        Latitude = c.Single(),
                        Longitude = c.Single(),
                    })
                .PrimaryKey(t => t.LID);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        RID = c.Int(nullable: false, identity: true),
                        BID = c.Int(nullable: false),
                        LID = c.Int(),
                    })
                .PrimaryKey(t => t.RID)
                .ForeignKey("dbo.VehicleInfoes", t => t.BID, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.LID)
                .Index(t => t.BID)
                .Index(t => t.LID);
            
            CreateTable(
                "dbo.VehicleInfoes",
                c => new
                    {
                        BID = c.Int(nullable: false, identity: true),
                        VehicleId = c.String(nullable: false),
                        OwnerName = c.String(nullable: false),
                        OwnerTel = c.String(),
                        OwnerAddr = c.String(),
                        SimNo = c.Int(),
                        VehType = c.Int(nullable: false),
                        RegisteredDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BID);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        TripID = c.Int(nullable: false, identity: true),
                        BID = c.Int(nullable: false),
                        StartID = c.Int(),
                        EndID = c.Int(),
                        time = c.Int(),
                        price = c.Int(),
                        Location_ID = c.Int(),
                    })
                .PrimaryKey(t => t.TripID)
                .ForeignKey("dbo.VehicleInfoes", t => t.BID, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.EndID)
                .ForeignKey("dbo.Locations", t => t.StartID)
                .ForeignKey("dbo.Locations", t => t.Location_ID)
                .Index(t => t.BID)
                .Index(t => t.StartID)
                .Index(t => t.EndID)
                .Index(t => t.Location_ID);
            
            CreateTable(
                "dbo.VehicleManages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AgentID = c.Guid(nullable: false),
                        BID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VehicleInfoes", t => t.BID, cascadeDelete: true)
                .Index(t => t.BID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        TripID = c.Int(),
                        StartID = c.Int(),
                        EndID = c.Int(),
                        SeatNo = c.Int(),
                        Price = c.Single(),
                        Location_ID = c.Int(),
                    })
                .PrimaryKey(t => t.TID)
                .ForeignKey("dbo.Locations", t => t.EndID)
                .ForeignKey("dbo.Locations", t => t.StartID)
                .ForeignKey("dbo.Trips", t => t.TripID)
                .ForeignKey("dbo.Locations", t => t.Location_ID)
                .Index(t => t.TripID)
                .Index(t => t.StartID)
                .Index(t => t.EndID)
                .Index(t => t.Location_ID);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UID = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        RegisteredDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "Location_ID", "dbo.Locations");
            DropForeignKey("dbo.Tickets", "Location_ID", "dbo.Locations");
            DropForeignKey("dbo.Tickets", "TripID", "dbo.Trips");
            DropForeignKey("dbo.Tickets", "StartID", "dbo.Locations");
            DropForeignKey("dbo.Tickets", "EndID", "dbo.Locations");
            DropForeignKey("dbo.Routes", "LID", "dbo.Locations");
            DropForeignKey("dbo.VehicleManages", "BID", "dbo.VehicleInfoes");
            DropForeignKey("dbo.Trips", "StartID", "dbo.Locations");
            DropForeignKey("dbo.Trips", "EndID", "dbo.Locations");
            DropForeignKey("dbo.Trips", "BID", "dbo.VehicleInfoes");
            DropForeignKey("dbo.Routes", "BID", "dbo.VehicleInfoes");
            DropIndex("dbo.Tickets", new[] { "Location_ID" });
            DropIndex("dbo.Tickets", new[] { "EndID" });
            DropIndex("dbo.Tickets", new[] { "StartID" });
            DropIndex("dbo.Tickets", new[] { "TripID" });
            DropIndex("dbo.VehicleManages", new[] { "BID" });
            DropIndex("dbo.Trips", new[] { "Location_ID" });
            DropIndex("dbo.Trips", new[] { "EndID" });
            DropIndex("dbo.Trips", new[] { "StartID" });
            DropIndex("dbo.Trips", new[] { "BID" });
            DropIndex("dbo.Routes", new[] { "LID" });
            DropIndex("dbo.Routes", new[] { "BID" });
            DropTable("dbo.UserInfoes");
            DropTable("dbo.Tickets");
            DropTable("dbo.VehicleManages");
            DropTable("dbo.Trips");
            DropTable("dbo.VehicleInfoes");
            DropTable("dbo.Routes");
            DropTable("dbo.Locations");
            DropTable("dbo.AgentInfoes");
            DropTable("dbo.AdminInfoes");
        }
    }
}
