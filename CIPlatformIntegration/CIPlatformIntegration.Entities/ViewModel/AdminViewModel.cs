using CIPlatformIntegration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Entities.ViewModel
{
   public class AdminViewModel
    {
        public List<User> users { get; set; }

        public User? User { get; set; }
    }
}
