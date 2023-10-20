namespace VehicleRentalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleAvailabilityTableupdate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VehicleAvailabilities", "VehicleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VehicleAvailabilities", "VehicleId", c => c.String());
        }
    }
}
