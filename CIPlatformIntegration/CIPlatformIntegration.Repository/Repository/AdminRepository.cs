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

        public void missionThemeEdit(long missionThemeId,string editTitle)
        {
            MissionTheme missionTheme = _cidatabaseContext.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == missionThemeId);

            missionTheme.Title = editTitle;
            missionTheme.Status = 1;
            missionTheme.UpdatedAt = DateTime.Now;
           

            _cidatabaseContext.MissionThemes.Update(missionTheme);
            _cidatabaseContext.SaveChanges();
        }


        //For Skills

        public AdminViewModel adminViewModelMainForMissionSkills()
        {
            AdminViewModel adminViewModelMainForSkillObject = new AdminViewModel
            {
                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(),
                users = _cidatabaseContext.Users.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.ToList(),
                missions = _cidatabaseContext.Missions.ToList(),
                stories = _cidatabaseContext.Stories.ToList(),
                missionThemes = _cidatabaseContext.MissionThemes.ToList(),
                skills= _cidatabaseContext.Skills.Where(s=>s.DeletedAt==null).ToList()
            };

            return adminViewModelMainForSkillObject;

        }

        public void missionSkillsAdd(string skillName)
        {
            Skill skill = new Skill
            {
                SkillName = skillName,
                Status = 1,
                CreatedAt = DateTime.Now,
            };

            _cidatabaseContext.Skills.Add(skill);
            _cidatabaseContext.SaveChanges();

        }


        public void missionSkillsEdit(long missionSkillsId, string editSkillName, int SelectStatus)
        {
            Skill skill = _cidatabaseContext.Skills.FirstOrDefault(s => s.SkillId == missionSkillsId);

            skill.SkillName = editSkillName;
            skill.Status = (byte)SelectStatus;
            skill.UpdatedAt = DateTime.Now;


            _cidatabaseContext.Skills.Update(skill);
            _cidatabaseContext.SaveChanges();

        }

        

        public void missionSkillsDelete(long missionSkillsId)
        {
            Skill skill = _cidatabaseContext.Skills.FirstOrDefault(s => s.SkillId == missionSkillsId);

            skill.Status = 0;
            skill.UpdatedAt = DateTime.Now;
            skill.DeletedAt = DateTime.Now;


            _cidatabaseContext.Skills.Update(skill);
            _cidatabaseContext.SaveChanges();

        }


        //For CMS Page

        public AdminViewModel adminViewModelMainForCMS()
        {
            AdminViewModel adminViewModelMainForCMSObject = new AdminViewModel
            {
                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(),
                users = _cidatabaseContext.Users.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.ToList(),
                missions = _cidatabaseContext.Missions.ToList(),
                stories = _cidatabaseContext.Stories.ToList(),
                missionThemes = _cidatabaseContext.MissionThemes.ToList(),
                skills = _cidatabaseContext.Skills.ToList(),
                cmsPages = _cidatabaseContext.CmsPages.ToList(),
            };

            return adminViewModelMainForCMSObject;
        }

        public void CMSAdd(string CMSTitle, string CMSDescription, string CMSSlug, int SelectStatus)
        {
            var cmsPage = new CmsPage
            {
                Title = CMSTitle,
                Description = CMSDescription,
                Slug = CMSSlug,
                Status = SelectStatus,
                CreatedAt = DateTime.Now,
            };

            _cidatabaseContext.Add(cmsPage);
            _cidatabaseContext.SaveChanges();
        }

        public void CMSEdit(long CMSId, string CMSTitleEdit, string CMSDescriptionEdit, string CMSSlugEdit, int SelectStatusEdit)
        {
            CmsPage cmsPage = _cidatabaseContext.CmsPages.FirstOrDefault(c => c.CmsPageId == CMSId);

            cmsPage.Title = CMSTitleEdit;
            cmsPage.Description = CMSDescriptionEdit;
            cmsPage.Status = SelectStatusEdit;
            cmsPage.Slug = CMSSlugEdit;
            cmsPage.UpdatedAt = DateTime.Now;


            _cidatabaseContext.CmsPages.Update(cmsPage);
            _cidatabaseContext.SaveChanges();

        }

        public void CMSApprove(long CMSId)
        {
            CmsPage cmsPage = _cidatabaseContext.CmsPages.FirstOrDefault(c => c.CmsPageId == CMSId);

            cmsPage.Status = 1;
            cmsPage.UpdatedAt = DateTime.Now;


            _cidatabaseContext.CmsPages.Update(cmsPage);
            _cidatabaseContext.SaveChanges();

        }

        public void CMSDelete(long CMSId)
        {
            CmsPage cmsPage = _cidatabaseContext.CmsPages.FirstOrDefault(c => c.CmsPageId == CMSId);

            cmsPage.Status = 0;
            cmsPage.UpdatedAt = DateTime.Now;
            cmsPage.DeletedAt = DateTime.Now;


            _cidatabaseContext.CmsPages.Update(cmsPage);
            _cidatabaseContext.SaveChanges();

        }

        //For Mission
        public AdminViewModel adminViewModelMainForMission()
        {
            AdminViewModel adminViewModelMainForMissionObject = new AdminViewModel
            {
                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(),
                users = _cidatabaseContext.Users.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.ToList(),
                missions = _cidatabaseContext.Missions.ToList(),
                stories = _cidatabaseContext.Stories.ToList(),
                missionThemes = _cidatabaseContext.MissionThemes.ToList(),
                skills = _cidatabaseContext.Skills.ToList(),
                cmsPages = _cidatabaseContext.CmsPages.ToList(),
            };

            return adminViewModelMainForMissionObject;
        }

        public AdminViewModel adminViewModelMainForAddMission(long missionId)
        {
            AdminViewModel adminViewModelMainForAddMissionObject = new AdminViewModel
            {
                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(),
                users = _cidatabaseContext.Users.ToList(),
                missionApplications = _cidatabaseContext.MissionApplications.ToList(),
                missions = _cidatabaseContext.Missions.ToList(),
                stories = _cidatabaseContext.Stories.ToList(),
                missionThemes = _cidatabaseContext.MissionThemes.Where(mt=>mt.Status==1).ToList(),
                skills = _cidatabaseContext.Skills.ToList(),
                cmsPages = _cidatabaseContext.CmsPages.ToList(),
                singleMission = _cidatabaseContext.Missions.Where(m=>m.MissionId==missionId).FirstOrDefault(),
            };

            return adminViewModelMainForAddMissionObject;
        }


        public AdminViewModel adminViewModelMainForAddMissionDetails(AdminViewModel adminViewModelMain)
        {
            Mission mission = new Mission()
            { 
                Title= adminViewModelMain.singleMission.Title,
                ShortDescription= adminViewModelMain.singleMission.ShortDescription,
                Description = adminViewModelMain.singleMission.Description,
                CountryId =adminViewModelMain.singleMission.CountryId,
                CityId = adminViewModelMain.singleMission.CityId,
                OrganizationName = adminViewModelMain.singleMission.OrganizationName,
                OrganizationDetail = adminViewModelMain.singleMission.OrganizationDetail,
                StartDate=adminViewModelMain.singleMission.StartDate,
                EndDate=adminViewModelMain.singleMission.EndDate,
                MissionType=adminViewModelMain.singleMission.MissionType,
                TotalSeats=adminViewModelMain.singleMission.TotalSeats,
                RegistrationDeadline=adminViewModelMain.singleMission.RegistrationDeadline,
                ThemeId=adminViewModelMain.singleMission.ThemeId,
               /* MissionSkills= adminViewModelMain.singleMission.MissionSkills,*/

            };
            _cidatabaseContext.Missions.Add(mission);
            _cidatabaseContext.SaveChanges();

            var missionId = mission.MissionId;
            MissionDocument missionDocument = new MissionDocument();
            
           foreach (var file in adminViewModelMain.Files)
            {
                missionDocument.MissionId= missionId;
                missionDocument.DocumentName= file.FileName;
                missionDocument.DocumentType = file.ContentType;

                FileStream FileStream = new(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Documents\\", Path.GetFileName(file.FileName)), FileMode.Create);
                                
                string path = "Documents\\" + Path.GetFileName(file.FileName);

                missionDocument.DocumentPath = path;

                _cidatabaseContext.MissionDocuments.Add(missionDocument);
                
                file.CopyToAsync(FileStream);                            

                FileStream.Close();

            }
             _cidatabaseContext.SaveChanges();

            return adminViewModelMain;

        }


    }
}
