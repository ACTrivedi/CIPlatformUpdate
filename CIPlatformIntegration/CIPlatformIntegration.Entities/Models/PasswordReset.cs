using System;
using System.Collections.Generic;
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

        public string Password { get; set; }

        [NotMapped]

        public string Confirmpassword { get; set; }





    }
}
