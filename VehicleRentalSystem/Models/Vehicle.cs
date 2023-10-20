using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleRentalSystem.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string RegistrationNumber { get; set; }

        public int RegistrationYear { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
        public string VehicleImage { get; set; }

        public string Colour { get; set; }

        public string Details { get; set; }

        public decimal Tariff { get; set; }

        public bool IsActive { get; set; }
    }
}