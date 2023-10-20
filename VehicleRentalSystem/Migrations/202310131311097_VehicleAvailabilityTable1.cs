namespace VehicleRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleAvailabilityTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleAvailabilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        IsBooked = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        VehicleId = c.String(),
                        Vehicle_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_Id)
                .Index(t => t.Vehicle_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleAvailabilities", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.VehicleAvailabilities", new[] { "Vehicle_Id" });
            DropTable("dbo.VehicleAvailabilities");
        }
    }
}
