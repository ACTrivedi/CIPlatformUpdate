using CIPlatformIntegration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Entities.ViewModel
{
    public class StoryDetailViewModel
    {
        public List<User> Users { get; set; }

        public List<Mission> Missions { get; set; }

        public List<Story> Stories { get; set; }

        public List<StoryMedium> storyMedia { get; set; }

        public List<User> recommendUser { get; set; }
    }
}
