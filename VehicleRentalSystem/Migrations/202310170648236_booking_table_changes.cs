namespace VehicleRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class booking_table_changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleBookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingNumber = c.String(),
                        AmountToBePaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdditionalCharges = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdvancePaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentMode = c.String(),
                        VehicleId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        VehicleAvailabilityId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ModifiedBy = c.Int(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleAvailabilities", t => t.VehicleAvailabilityId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.UserId)
                .Index(t => t.VehicleAvailabilityId);
            
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleBookings", "VehicleAvailabilityId", "dbo.VehicleAvailabilities");
            DropForeignKey("dbo.VehicleBookings", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.VehicleBookings", "UserId", "dbo.Users");
            DropIndex("dbo.VehicleBookings", new[] { "VehicleAvailabilityId" });
            DropIndex("dbo.VehicleBookings", new[] { "UserId" });
            DropIndex("dbo.VehicleBookings", new[] { "VehicleId" });
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            DropTable("dbo.VehicleBookings");
        }
    }
}
