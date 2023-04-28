using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatformIntegration.Repository.Interface
{
    public interface IAdminRepository
    {
        User user(long userIdForUserEdit);

        AdminViewModel adminViewModelMain();

        AdminViewModel adminViewModel(User userUpdate,long userIdForUserEdit);

        User userCheck(long userIdCheckForEdit);

        bool userAdd(int countryid, int cityid, string name, string surname, string email, string password, IFormFile avatar, string employee_id, string department, string profile_text);

        bool userEdit(long userIdCheckForEdit, int countryid, int cityid, string name, string surname, string email, string password, IFormFile avatar, string employee_id, string department, string profile_text, int status);

        List<object> userEditData(long selectedUserId);             

        bool adminUserDelete(long selectedUserId);


        //MissionApplication

        void AdminMissionApplicationApprove(long missionApplicationId);

        void AdminMissionApplicationDelete(long missionApplicationId);

        //Story

        AdminViewModel adminViewModelMainForStory();

        AdminViewModel adminViewModelMainForStoryDetail(long storyId);

        void storyDelete(long storyId);
        void storyDecline(long storyId);
        void storyApprove(long storyId);

        //Mission Theme

        AdminViewModel adminViewModelMainForMissionTheme();

        void missionThemeAdd(string Title);
        
        void missionThemeApprove(long missionThemeId);

        void missionThemeDelete(long missionThemeId);

        void missionThemeEdit(long missionThemeId,string editTitle);


        //Mission Skills

        AdminViewModel adminViewModelMainForMissionSkills();

        void missionSkillsAdd(string skillName);

        void missionSkillsEdit(long missionSkillsId, string editSkillName,int SelectStatus);

       

        void missionSkillsDelete(long missionSkillsId);


        //CMS

        AdminViewModel adminViewModelMainForCMS();

        void CMSAdd(string CMSTitle, string CMSDescription, string CMSSlug, int SelectStatus);

        void CMSEdit(long CMSId, string CMSTitleEdit, string CMSDescriptionEdit, string CMSSlugEdit, int SelectStatusEdit);
        void CMSApprove(long CMSId);

        void CMSDelete(long CMSId);


        //For mission main
        AdminViewModel adminViewModelMainForMission();

        AdminViewModel adminViewModelMainForAddMission(long missionId);

        AdminViewModel adminViewModelMainForAddMissionDetails(AdminViewModel adminViewModelMain, long[] Skilllist, List<IFormFile> defaultImage, List<IFormFile> missionImages, string MissionVideoURL, string goalObjectiveText, int goalValue);
    
        AdminViewModel adminViewModelMainForEditMission(long missionId);

        AdminViewModel adminViewModelMainForDeleteMission(long missionId);

        //For Mission Images
        /* List<IFormFile> GetImageFileList(long missionId);*/

       
    }
}
