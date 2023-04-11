using CIPlatformIntegration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Entities.ViewModel
{
    public class UserEditProfileViewModel
    {

        public List<User> Users { get; set; }

        public List<Mission> Missions { get; set; }

        public List<Story> Stories { get; set; }

        public List<StoryMedium> storyMedia { get; set; }

        public User IndividualUser { get; set; }

        public List<Skill> skills { get; set; } 

        public List<UserSkill> userSkills { get; set; } 

        public List<Country> countries { get; set; }
    }
}
