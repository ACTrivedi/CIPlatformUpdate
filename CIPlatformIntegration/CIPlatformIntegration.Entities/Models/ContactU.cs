using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CIPlatformIntegration.Entities.Models
{
    public partial class ContactU
    {        
        public long ContactUsId { get; set; }
        public long? UserId { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        public string? Subject { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
