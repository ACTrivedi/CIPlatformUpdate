using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Entities.ViewModel;
using CIPlatformIntegration.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Repository.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CidatabaseContext _cidatabaseContext;

        public AdminRepository(CidatabaseContext cidatabaseContext)
        {
            _cidatabaseContext = cidatabaseContext;
        }


        public User user(long userIdForUserEdit)
        {
            var userUpdate = _cidatabaseContext.Users.Where(u => u.UserId == userIdForUserEdit).FirstOrDefault();
            return userUpdate;
        }

        public AdminViewModel adminViewModelMain()
        {

            AdminViewModel adminViewModelMainObject = new AdminViewModel
            {
                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(),
                users = _cidatabaseContext.Users.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.Where(ma => ma.ApprovalStatus == "PENDING").ToList(),
                missions = _cidatabaseContext.Missions.ToList(),
            };

            return adminViewModelMainObject;

        }



        public AdminViewModel adminViewModel(User userUpdate, long userIdForUserEdit)
        {

            AdminViewModel adminViewModelObject = new AdminViewModel
            {
                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.Where(c => c.CityId == userUpdate.CityId).ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(u => u.UserId == userIdForUserEdit),
                users = _cidatabaseContext.Users.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.Where(ma => ma.ApprovalStatus == "PENDING").ToList(),
                missions = _cidatabaseContext.Missions.ToList(),
            };

            return adminViewModelObject;

        }


        public User userCheck(long userIdCheckForEdit)
        {
            var userId = _cidatabaseContext.Users.FirstOrDefault(u => u.UserId == userIdCheckForEdit);
            return userId;

        }

        public bool userAdd(int countryid, int cityid, string name, string surname, string email, string password, IFormFile avatar, string employee_id, string department, string profile_text)
        {
            User user = new User
            {
                FirstName = name,
                LastName = surname,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),

                EmployeeId = employee_id,
                Department = department,
                ProfileText = profile_text,
                CityId = cityid,
                CountryId = countryid,

            };
            if (avatar != null)
            {
                string imageURL = "\\images\\" + Path.GetFileName(avatar.FileName);
                user.Avatar = imageURL;

            }

            _cidatabaseContext.Users.Add(user);
            _cidatabaseContext.SaveChanges();

            return true;




        }


        public bool userEdit(long userIdCheckForEdit, int countryid, int cityid, string name, string surname, string email, string password, IFormFile avatar, string employee_id, string department, string profile_text, int status)
        {
            User user = _cidatabaseContext.Users.FirstOrDefault(u => u.UserId == userIdCheckForEdit);

            user.FirstName = name;
            user.LastName = surname;
            user.Email = email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);

            user.EmployeeId = employee_id;
            user.Department = department;
            user.ProfileText = profile_text;
            user.CityId = cityid;
            user.CountryId = countryid;

            if (status == 1)
            {
                user.Status = status;
                user.DeletedAt = null;
            }
            if (status == 0)
            {
                user.Status = status;
                user.DeletedAt = DateTime.Now;
            }

            if (avatar != null)
            {
                string imageURL = "\\images\\" + Path.GetFileName(avatar.FileName);
                user.Avatar = imageURL;

            }

            _cidatabaseContext.Users.Update(user);
            _cidatabaseContext.SaveChanges();

            return true;




        }


        public List<object> userEditData(long selectedUserId)
        {
            var userEditData = from user in _cidatabaseContext.Users
                               join city in _cidatabaseContext.Cities
                               on user.CityId equals city.CityId
                               where user.UserId == selectedUserId
                               select new
                               { // result selector 
                                   Firstname = user.FirstName,
                                   Lastname = user.LastName,
                                   Email = user.Email,
                                   Phonenumber = user.PhoneNumber,
                                   avatar = user.Avatar,
                                   manager = user.Manager,
                                   avalibility = user.Availability,
                                   employee_id = user.EmployeeId,
                                   department = user.Department,
                                   country_id = user.CountryId,
                                   city_id = user.CityId,
                                   profile_text = user.ProfileText,
                                   status = user.Status,
                                   city_name = city.Name,
                                   selectedUserId = selectedUserId,

                               };

            return userEditData.Cast<object>().ToList();
        }


        public bool adminUserDelete(long selectedUserId)
        {
            User user = _cidatabaseContext.Users.FirstOrDefault(u => u.UserId == selectedUserId);

            if (user.Status == 1)
            {
                user.Status = 0;
                user.DeletedAt = DateTime.Now;

                _cidatabaseContext.Users.Update(user);
                _cidatabaseContext.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }


        public void AdminMissionApplicationApprove(long missionApplicationId)
        {
            var appliedMission = _cidatabaseContext.MissionApplications.FirstOrDefault(ma => ma.MissionApplicationId == missionApplicationId);

            appliedMission.ApprovalStatus = "APPROVED";
            appliedMission.UpdatedAt = DateTime.Now;

            _cidatabaseContext.MissionApplications.Update(appliedMission);
            _cidatabaseContext.SaveChanges();



        }

        public void AdminMissionApplicationDelete(long missionApplicationId)
        {
            var appliedMission = _cidatabaseContext.MissionApplications.FirstOrDefault(ma => ma.MissionApplicationId == missionApplicationId);

            appliedMission.ApprovalStatus = "DECLINE";
            appliedMission.UpdatedAt = DateTime.Now;
            appliedMission.DeletedAt = DateTime.Now;

            _cidatabaseContext.MissionApplications.Update(appliedMission);
            _cidatabaseContext.SaveChanges();


        }

        //For Story


        public AdminViewModel adminViewModelMainForStory()
        {

            AdminViewModel adminViewModelMainForStoryObject = new AdminViewModel
            {
                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(),
                users = _cidatabaseContext.Users.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.ToList(),
                missions = _cidatabaseContext.Missions.ToList(),
                stories = _cidatabaseContext.Stories.Where(s => s.Status == "DRAFT").ToList(),
            };

            return adminViewModelMainForStoryObject;

        }


        public AdminViewModel adminViewModelMainForStoryDetail(long storyId)
        {
            AdminViewModel adminViewModelMainForStoryObject = new AdminViewModel
            {
                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(),
                users = _cidatabaseContext.Users.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.ToList(),
                missions = _cidatabaseContext.Missions.ToList(),
                story = _cidatabaseContext.Stories.Where(s => s.StoryId == storyId).FirstOrDefault(),
                StoryMedium = _cidatabaseContext.StoryMedia.Where(sm => sm.StoryId == storyId).ToList(),

            };

            return adminViewModelMainForStoryObject;

        }


        public void storyDelete(long storyId)
        {
            Story story = _cidatabaseContext.Stories.FirstOrDefault(s => s.StoryId == storyId);
            if (story != null)
            {
                story.Status = "DECLINED";
                story.DeletedAt = DateTime.Now;
                story.UpdatedAt = DateTime.Now;

                _cidatabaseContext.Stories.Update(story);
                _cidatabaseContext.SaveChanges();
            }
        }

        public void storyDecline(long storyId)
        {
            Story story = _cidatabaseContext.Stories.FirstOrDefault(s => s.StoryId == storyId);
            if (story != null)
            {
                story.Status = "DECLINED";
                story.UpdatedAt = DateTime.Now;


                _cidatabaseContext.Stories.Update(story);
                _cidatabaseContext.SaveChanges();
            }
        }

        public void storyApprove(long storyId)
        {
            Story story = _cidatabaseContext.Stories.FirstOrDefault(s => s.StoryId == storyId);
            if (story != null)
            {
                story.Status = "APPROVED";

                story.UpdatedAt = DateTime.Now;

                _cidatabaseContext.Stories.Update(story);
                _cidatabaseContext.SaveChanges();
            }
        }

        //For Mission Theme

        public AdminViewModel adminViewModelMainForMissionTheme()
        {

            AdminViewModel adminViewModelMainForStoryObject = new AdminViewModel
            {
                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(),
                users = _cidatabaseContext.Users.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.ToList(),
                missions = _cidatabaseContext.Missions.ToList(),
                stories = _cidatabaseContext.Stories.ToList(),
                missionThemes = _cidatabaseContext.MissionThemes.ToList(),
            };

            return adminViewModelMainForStoryObject;

        }

        public void missionThemeAdd(string Title)
        {
            MissionTheme missionTheme = new MissionTheme
            {
                Title = Title,
                Status = 1,
                CreatedAt = DateTime.Now,
            };

            _cidatabaseContext.MissionThemes.Add(missionTheme);
            _cidatabaseContext.SaveChanges();
        }

        public void missionThemeApprove(long missionThemeId)
        {
            MissionTheme missionTheme = _cidatabaseContext.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == missionThemeId);

            missionTheme.Status = 1;
            missionTheme.UpdatedAt = DateTime.Now;


            _cidatabaseContext.MissionThemes.Update(missionTheme);
            _cidatabaseContext.SaveChanges();
        }

        public void missionThemeDelete(long missionThemeId)
        {
            MissionTheme missionTheme = _cidatabaseContext.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == missionThemeId);

            missionTheme.Status = 0;
            missionTheme.UpdatedAt = DateTime.Now;
            missionTheme.DeletedAt = DateTime.Now;


            _cidatabaseContext.MissionThemes.Update(missionTheme);
            _cidatabaseContext.SaveChanges();
        }
    }
}
