using CIPlatformIntegration.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Entities.ViewModel
{
    public class HomePageViewModel
    {

        public int id { get; set; }
        public List<City> City { get; set; }
        public List<Country> Country { get; set; }
        public List<MissionTheme> MissionThemes { get; set; }
        public List<MissionSkill> MissionSkills { get; set; }
        public List<MissionRating> MissionRatings { get; set; }

        public List<Mission> RelatedMissions { get; set; }
        public List<Comment> Comments { get; set; }
        public List<FavoriteMission> FavoriteMissions { get; set; }
        public List<Mission> Missions { get; set; }
        public List<GoalMission> GoalMission { get; set; }

        public List<MissionMedium> missionMedia { get; set; }

        public List<User> usersRecommend { get; set; }

        public User Users { get; set; }

        public List<Timesheet> timesheets { get; set; }

        public List<Skill> skills { get; set; }

        //For Mission Count
        public int missionCount { get; set; }

        //For Pager
        public Pager pagerCount { get; set; }

        public List<MissionApplication> missionApplications { get; set; }

        public bool IsFavorite { get; set; }


       

    }
}
