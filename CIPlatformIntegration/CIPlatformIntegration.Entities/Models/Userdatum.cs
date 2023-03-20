using System;
using System.Collections.Generic;

namespace CIPlatformIntegration.Entities.Models
{
    public partial class Userdatum
    {
        public long UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
