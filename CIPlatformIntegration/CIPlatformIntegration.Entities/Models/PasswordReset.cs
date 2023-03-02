using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CIPlatformIntegration.Entities.Models
{
    public partial class PasswordReset
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public byte[]? CreatedAt { get; set; }
        public int Id { get; set; }


        [NotMapped]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [NotMapped]
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Confirmpassword { get; set; } = null!;


    }
}
