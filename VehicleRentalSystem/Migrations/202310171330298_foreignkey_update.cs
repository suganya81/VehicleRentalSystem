namespace VehicleRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkey_update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleAvailabilities", "Vehicle_Id", "dbo.Vehicles");
            DropIndex("dbo.VehicleAvailabilities", new[] { "Vehicle_Id" });
            RenameColumn(table: "dbo.VehicleAvailabilities", name: "Vehicle_Id", newName: "VehicleId");
            AddColumn("dbo.Vehicles", "VehicleImage", c => c.String());
            AlterColumn("dbo.VehicleAvailabilities", "VehicleId", c => c.Int(nullable: false));
            CreateIndex("dbo.VehicleAvailabilities", "VehicleId");
            AddForeignKey("dbo.VehicleAvailabilities", "VehicleId", "dbo.Vehicles", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleAvailabilities", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.VehicleAvailabilities", new[] { "VehicleId" });
            AlterColumn("dbo.VehicleAvailabilities", "VehicleId", c => c.Int());
            DropColumn("dbo.Vehicles", "VehicleImage");
            RenameColumn(table: "dbo.VehicleAvailabilities", name: "VehicleId", newName: "Vehicle_Id");
            CreateIndex("dbo.VehicleAvailabilities", "Vehicle_Id");
            AddForeignKey("dbo.VehicleAvailabilities", "Vehicle_Id", "dbo.Vehicles", "Id");
        }
    }
}
