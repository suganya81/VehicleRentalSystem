using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VehicleRentalSystem.Models;

namespace VehicleRentalSystem.Context
{
    public class VehicleRentalContext : DbContext
    {
        public VehicleRentalContext() : base("VehicleRentalContext")
        {

        }

        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleAvailability> VehicleAvailabilities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public virtual DbSet<VehicleBooking> VehicleBookings { get; set; }
    }
}