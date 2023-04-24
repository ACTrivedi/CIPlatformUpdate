using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using CIPlatformIntegration.Repository.Interface;

namespace CIPlatformIntegration.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        //CIDatabaseContext _cidatabaseContext = new CIDatabaseContext();
        private readonly IAdminRepository _adminRepository;
        private readonly CidatabaseContext _cidatabaseContext;


        public AdminController(ILogger<AdminController> logger, CidatabaseContext cIDatabaseContext, IAdminRepository repository)
        {
            _logger = logger;
            _cidatabaseContext = cIDatabaseContext;
            _adminRepository = repository;
        }

        [HttpGet]
        public IActionResult AdminPage()
        {

            var userIdForUserEdit = (long)HttpContext.Session.GetInt32("farfavuserid");

            var userUpdate = _adminRepository.user(userIdForUserEdit);


            AdminViewModel adminViewModel = _adminRepository.adminViewModel(userUpdate, userIdForUserEdit);

            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult AdminPage(int something)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminAddUser(long userIdCheckForEdit, int countryid, int cityid, string name, string surname, string email, string password, IFormFile avatar, string employee_id, string department, string profile_text, int status)
        {


            var userExistss = _adminRepository.userCheck(userIdCheckForEdit);

            if (userExistss == null)
            {
                var userAdded = _adminRepository.userAdd(countryid, cityid, name, surname, email, password, avatar, employee_id, department, profile_text);
                return RedirectToAction("AdminPage");


            }
            else
            {
                var userUpdated = _adminRepository.userEdit(userIdCheckForEdit, countryid, cityid, name, surname, email, password, avatar, employee_id, department, profile_text, status);
                return RedirectToAction("AdminPage");
            }


        }

        [HttpPost]
        public IActionResult missionApplicationMain()
        {

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMain();

            return PartialView("_adminMissionApplication", adminViewModelMain);

        }



        [HttpPost]
        public IActionResult AdminMissionApplicationApprove(long missionApplicationId)
        {

            _adminRepository.AdminMissionApplicationApprove(missionApplicationId);

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMain();

            return PartialView("_adminMissionApplication", adminViewModelMain);

        }


        [HttpPost]
        public IActionResult AdminMissionApplicationDelete(long missionApplicationId)
        {

            _adminRepository.AdminMissionApplicationDelete(missionApplicationId);

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMain();

            return PartialView("_adminMissionApplication", adminViewModelMain);



        }



        //For City
        [HttpPost]
        public JsonResult GetCitiesByCountryId(int Country_id)
        {
            var cities = _cidatabaseContext.Cities.Where(c => c.CountryId == Country_id).ToList();

            return Json(cities);
        }



        [HttpPost]

        public IActionResult userEditData(long selectedUserId)
        {
            var userSearch = _adminRepository.userEditData(selectedUserId);

            return Json(userSearch);

        }


        [HttpPost]
        public IActionResult adminUserDelete(long selectedUserId)
        {
            var userUpdated = _adminRepository.adminUserDelete(selectedUserId);

            return RedirectToAction("AdminPage");


        }

        //Story

        [HttpPost]
        public IActionResult storyMain()
        {

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForStory();

            return PartialView("_adminStory", adminViewModelMain);

        }

        [HttpPost]
        public IActionResult storyDetail(long storyId)
        {


            AdminViewModel story = _adminRepository.adminViewModelMainForStoryDetail(storyId);

            return PartialView("_adminStoryDetail", story);

        }

        [HttpPost]
        public IActionResult storyDelete(long storyId)
        {

            _adminRepository.storyDelete(storyId);

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForStory();

            return PartialView("_adminStory", adminViewModelMain);

        }


        [HttpPost]
        public IActionResult storyDecline(long storyId)
        {


            _adminRepository.storyDecline(storyId);

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForStory();

            return PartialView("_adminStory", adminViewModelMain);

        }

        [HttpPost]
        public IActionResult storyApprove(long storyId)
        {


            _adminRepository.storyApprove(storyId);

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForStory();

            return PartialView("_adminStory", adminViewModelMain);

        }

        [HttpPost]

        public IActionResult missionThemeMain()
        {
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);
        }


        [HttpPost]
        public IActionResult AdminAddMissionTheme(string Title)
        {

            _adminRepository.missionThemeAdd(Title);
            return RedirectToAction("AdminPage");

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);

        }

        

        [HttpPost]
        public IActionResult missionThemeApprove(long missionThemeId)
        {

            _adminRepository.missionThemeApprove(missionThemeId);
            return RedirectToAction("AdminPage");

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);

        }
                

        [HttpPost]
        public IActionResult missionThemeDelete(long missionThemeId)
        {

            _adminRepository.missionThemeDelete(missionThemeId);
            return RedirectToAction("AdminPage");

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);

        }




    }
}
