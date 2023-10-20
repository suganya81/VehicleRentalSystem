using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VehicleRentalSystem.Models
{
    public partial class Role
    {
        [Key]
        public int Id { get; set; }

        public string RoleName { get; set; }
    }
}