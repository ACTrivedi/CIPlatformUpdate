using CIPlatformIntegration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Entities.ViewModel
{
    public class VolunteeringTimesheetViewModel
    {
        public List<Timesheet> timesheets { get; set; }

        public List<Mission> missions { get; set; } 

        public List<MissionApplication> missionsApplication { get; set; }

        
    }
}
