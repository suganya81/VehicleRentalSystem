using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VehicleRentalSystem.Models
{
    public class VehicleBooking
    {
        [Key]
        public int Id { get; set; }
        public string BookingNumber { get; set; }
        public decimal AmountToBePaid { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal AdvancePaid { get; set; }
        public decimal Total { get; set; }
        public string PaymentMode { get; set; }

        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("VehicleAvailability")]
        public int VehicleAvailabilityId { get; set; }
        public virtual VehicleAvailability VehicleAvailability { get; set; }


        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}