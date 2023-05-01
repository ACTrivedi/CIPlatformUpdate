using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Entities.ViewModel;
using CIPlatformIntegration.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Repository.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly CidatabaseContext _cidatabaseContext;

        public UserRepository(CidatabaseContext cidatabaseContext)
        {
            _cidatabaseContext = cidatabaseContext;
        }

       

        public int userRegistration(User user)
        {
 
            var useremailverify = _cidatabaseContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (useremailverify != null)
            {
                return 3;
                
            }
            else 
            {
                User data = new User();
                data.Email = user.Email;
                if (user.Password != null)
                {
                    data.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }
                

                data.PhoneNumber = user.PhoneNumber;
                data.CityId = 1;
                data.CountryId = 1;
                data.LastName = user.LastName;
                data.FirstName = user.FirstName;
                data.Avatar = "\\images\\AvatarImages\\ProfileDefault.png";

                if (data.FirstName != null && data.LastName != null && data.PhoneNumber != null && data.Email != null && data.Password != null)
                {
                    _cidatabaseContext.Users.Add(data);
                    _cidatabaseContext.SaveChanges();
                    return 1;
                }
                else
                {
                    return 2;
                }

                
            }


        }

        public User userLogin(User _user)
        {
            if (_user.Email != null && _user.Password != null)
            {
                var emailstatus = _cidatabaseContext.Users.FirstOrDefault(m => m.Email == _user.Email);

                bool verified = BCrypt.Net.BCrypt.Verify(_user.Password, emailstatus.Password);
                var status = _cidatabaseContext.Users.Where(m => m.Email == _user.Email && verified ).FirstOrDefault();
                if (status == null)
                {
                    return null;
                }
                else
                {
                    

                    return status;
                }
            }
            return null;
        }



        public HomePageViewModel homePageViewModel(long userIdForFav)
        {
            HomePageViewModel model = new HomePageViewModel
            {
                Missions = _cidatabaseContext.Missions.ToList(),
                Country = _cidatabaseContext.Countries.ToList(),
                City = _cidatabaseContext.Cities.ToList(),
                MissionThemes = _cidatabaseContext.MissionThemes.ToList(),
                MissionSkills = _cidatabaseContext.MissionSkills.ToList(),
                GoalMission = _cidatabaseContext.GoalMissions.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.ToList(),
                FavoriteMissions = _cidatabaseContext.FavoriteMissions.ToList(),
                MissionRatings=_cidatabaseContext.MissionRatings.ToList(),
                Users = _cidatabaseContext.Users.FirstOrDefault(u => u.UserId == userIdForFav),
                timesheets =_cidatabaseContext.Timesheets.ToList(),
                skills=_cidatabaseContext.Skills.ToList(),
                missionMedia=_cidatabaseContext.MissionMedia.ToList(),
               
            };

            return model;
        }



        public HomePageViewModel filtering(long userIdForFav,string[]? country, string[]? city, string[]? theme,string[]? skills, string? searchTerm, string? sortValue, int pg)
        {

            HomePageViewModel model = new HomePageViewModel
            {
                Missions = _cidatabaseContext.Missions.ToList(),
                Country = _cidatabaseContext.Countries.ToList(),
                City = _cidatabaseContext.Cities.ToList(),
                MissionThemes = _cidatabaseContext.MissionThemes.ToList(),
                MissionSkills = _cidatabaseContext.MissionSkills.ToList(),
                GoalMission = _cidatabaseContext.GoalMissions.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.ToList(),
                FavoriteMissions = _cidatabaseContext.FavoriteMissions.ToList(),
                MissionRatings = _cidatabaseContext.MissionRatings.ToList(),
                Users = _cidatabaseContext.Users.FirstOrDefault(u => u.UserId == userIdForFav),
                timesheets = _cidatabaseContext.Timesheets.ToList(),
                skills = _cidatabaseContext.Skills.ToList(),
                missionMedia = _cidatabaseContext.MissionMedia.ToList(),
            };

            List<Mission> miss = _cidatabaseContext.Missions.ToList();

           

            if (country.Count() > 0 || city.Count() > 0 || theme.Count() > 0 || skills.Count()>0)
            {
                miss = GetFilteredMission(miss, country, city, theme, skills);
            }


            if (searchTerm != null)
            {
                miss = miss.Where(m => m.Title.ToLower().Contains(searchTerm)).ToList();
            }

            miss = GetSortedMissions(miss, sortValue);

            int totalCount = miss.Count();

            model.missionCount= totalCount;

            /*ViewBag.TotalCount = totalCount;*/
            model.Missions = miss;



            //Extra Code for the Pagination

            const int pageSize = 9;
            if (pg < 1)
                pg = 1;

            int recsCount = model.Missions.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = model.Missions.Skip(recSkip).Take(pager.PageSize).ToList();

            /* this.ViewBag.Pager = pager;*/

            model.pagerCount = pager;

            model.Missions = data.ToList();



            return model;
        }


        public List<Mission> GetSortedMissions(List<Mission> miss, string sortValue)
        {
            switch (sortValue)
            {
                case "Newest":
                    return miss.OrderByDescending(m => m.StartDate).ToList();
                case "Oldest":
                    return miss.OrderBy(m => m.StartDate).ToList();
                case "lowest":
                    return miss.OrderBy(m => m.TotalSeats).ToList();
                case "highest":
                    return miss.OrderByDescending(m => m.TotalSeats).ToList();
                default:
                    return miss.ToList();

            }
            
        }

        public List<Mission> GetFilteredMission(List<Mission> miss, string[] country, string[] city, string[] theme, string[]? skills)
        {
            if (country.Length > 0)
            {
                miss = miss.Where(m => country.Contains(m.Country.Name)).ToList();
            }

            if (city.Length > 0)
            {
                miss = miss.Where(m => city.Contains(m.City.Name)).ToList();
            }

            if (theme.Length > 0)
            {
                miss = miss.Where(m => theme.Contains(m.Theme.Title)).ToList();
            }

            if (skills.Length > 0)
            {              
                var SkillSkillIdArr = new List<long>();
                foreach (var i in skills)
                {                     
                var skillId=_cidatabaseContext.Skills.Where(s=>s.SkillName==i).FirstOrDefault().SkillId;

                   var missionSkill = _cidatabaseContext.MissionSkills.Where(ms => ms.SkillId == skillId).FirstOrDefault();
                    if (missionSkill != null)
                    { 
                    
                    var missionSkillId = _cidatabaseContext.MissionSkills.Where(ms => ms.SkillId == skillId).FirstOrDefault().MissionId;
                    SkillSkillIdArr.Add((long)missionSkillId);
                    }
                }


                miss = miss.Where(m => SkillSkillIdArr.Contains(m.MissionId)).ToList();
            }



            return miss;
        }








    }
}

