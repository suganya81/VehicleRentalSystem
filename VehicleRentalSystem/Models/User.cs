using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace VehicleRentalSystem.Models
{
    public partial class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required]
        public string Email { get; set; }

        [MembershipPassword(
        MinRequiredNonAlphanumericCharacters = 1,
        MinNonAlphanumericCharactersError = "Your password needs to contain at least one symbol (!, @, #, etc).",
        ErrorMessage = "Your password must be 6 characters long and contain at least one symbol (!, @, #, etc).",
        MinRequiredPasswordLength = 6
         )]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
        [DisplayName("Is Locked Out")]
        public bool IsLockedOut { get; set; }
        [DisplayName("Account Creation Date")]
        public DateTime CreatedAt { get; set; }
    }
}