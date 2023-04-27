using CIPlatformIntegration.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Entities.ViewModel
{
   public class AdminViewModel
    {

        public List<City> citiesForEdit { get; set; }   
        public List<User> users { get; set; }

        public User? User { get; set; }

        public List<Country> countries { get; set; }       

        public List<City> cities { get; set; }

        public List<Mission> missions { get; set; }

        public IList<MissionApplication> missionApplications { get; set; }

        public List<Story> stories { get; set; }

        //For story View
        public Story story { get; set; }

        public List<StoryMedium> StoryMedium { get; set; }

        //For mission theme 
        public List<MissionTheme> missionThemes { get; set; }

        //For mission skills
        public List<Skill> skills { get; set; }
    
        //For CMS
        public List<CmsPage> cmsPages { get; set; }

        //For Mission
        public Mission singleMission { get; set; }

        public MissionMedium missionMedium { get; set; }

        public List<IFormFile> Files { get; set; }

        public List<GoalMission> goalMissions { get; set; } 




    }
}
