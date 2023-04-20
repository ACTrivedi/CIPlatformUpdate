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

        AdminViewModel adminViewModel(User userUpdate,long userIdForUserEdit);

        User userCheck(long userIdCheckForEdit);

        bool userAdd(int countryid, int cityid, string name, string surname, string email, string password, IFormFile avatar, string employee_id, string department, string profile_text);

        bool userEdit(long userIdCheckForEdit, int countryid, int cityid, string name, string surname, string email, string password, IFormFile avatar, string employee_id, string department, string profile_text, int status);

        List<object> userEditData(long selectedUserId);             

        bool adminUserDelete(long selectedUserId);


        //MissionApplication

        bool AdminMissionApplicationApprove(long missionApplicationId);

        bool AdminMissionApplicationDelete(long missionApplicationId);



    }
}
