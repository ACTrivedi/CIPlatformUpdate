using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Entities.ViewModel
{
    public class MissionData
    {
        public string Title { get; set; } = null!;

        public string? ShortDescription { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string? MissionTheme { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
       
       
       
      
        public string MissionType { get; set; } = null!;
        public int Status { get; set; }
        public string? OrganizationName { get; set; }
        public string? OrganizationDetail { get; set; }


    }
}
