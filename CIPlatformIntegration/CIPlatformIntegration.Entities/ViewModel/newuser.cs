using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Entities.ViewModel
{
    public class newuser
    {

        public long UserId { get; set; }

        public string? FirstName { get; set; }

        
        public string? LastName { get; set; }

        
        public string Email { get; set; } = null!;
    }
}
