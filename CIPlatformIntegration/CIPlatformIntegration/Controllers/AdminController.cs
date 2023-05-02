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

        //For Theme

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

        [HttpPost]

        public IActionResult missionThemeEditButton(long missionThemeId)
        {
            var missionThemeTitle = _cidatabaseContext.MissionThemes.FirstOrDefault(mt => mt.MissionThemeId == missionThemeId).Title;
            return Json(missionThemeTitle);
        }

        [HttpPost]
        public IActionResult AdminEditMissionTheme(long missionThemeId, string editTitle)
        {

            _adminRepository.missionThemeEdit(missionThemeId, editTitle);
            return RedirectToAction("AdminPage");

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);

        }


        //Skill

        [HttpPost]
        
        public IActionResult missionSkillsMain()
        {
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionSkills();

            return PartialView("_adminMissionSkills", adminViewModelMain);
        }

        
        [HttpPost]
        public IActionResult AdminAddMissionSkills(string skillName)
        {
            _adminRepository.missionSkillsAdd(skillName);
            return RedirectToAction("AdminPage");

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionSkills();

            return PartialView("_adminMissionSkills", adminViewModelMain);

        }

        

        [HttpPost]

        public IActionResult missionSkillEditButton(long missionSkillId)
        {
            var missionSkillName = _cidatabaseContext.Skills.FirstOrDefault(s => s.SkillId == missionSkillId).SkillName;

            return Json(missionSkillName);
        }
        
         [HttpPost]
        public IActionResult AdminEditMissionSkills(long missionSkillsId, string editSkillName,int SelectStatus)
        {

            _adminRepository.missionSkillsEdit(missionSkillsId, editSkillName, SelectStatus);
            return RedirectToAction("AdminPage");

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);

        }



        
        [HttpPost]
        public IActionResult missionSkillsDelete(long missionSkillsId)
        {

            _adminRepository.missionSkillsDelete(missionSkillsId);
            return RedirectToAction("AdminPage");

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);

        }


        //For CMS
        
        [HttpPost]

        public IActionResult CMSMain()
        {
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForCMS();

            return PartialView("_adminCMS", adminViewModelMain);
        }

        
        [HttpPost]
        public IActionResult AdminAddCMS(string CMSTitle, string CMSDescription, string CMSSlug, int SelectStatus)
        {
            _adminRepository.CMSAdd( CMSTitle,  CMSDescription,  CMSSlug,  SelectStatus);
           

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForCMS();

            return PartialView("_adminCMS", adminViewModelMain);

        }

        
        [HttpPost]
        public IActionResult CMSEditButton(long CMSId)
        {
            var CMSData = _cidatabaseContext.CmsPages.FirstOrDefault(c => c.CmsPageId == CMSId);
            var data = new  {
                CMSTitle=CMSData.Title,
                CMSDescription=CMSData.Description,
                CMSSlug=CMSData.Slug,
                SelectStatus=CMSData.Status
            };
            return Json(data) ;
        }

        [HttpPost]
        public IActionResult AdminEditCMS(long CMSId,string CMSTitleEdit, string  CMSDescriptionEdit,string CMSSlugEdit,int SelectStatusEdit)
        {
            _adminRepository.CMSEdit( CMSId,  CMSTitleEdit,   CMSDescriptionEdit,  CMSSlugEdit,  SelectStatusEdit);
            return RedirectToAction("AdminPage");

            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);
        }

        
        [HttpPost]
        public IActionResult CMSApprove(long CMSId)
        {
            _adminRepository.CMSApprove(CMSId);
           
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);

        }


        [HttpPost]
        public IActionResult CMSDelete(long CMSId)
        {
            _adminRepository.CMSDelete(CMSId);
           
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMissionTheme();

            return PartialView("_adminMissionTheme", adminViewModelMain);

        }

        // For mission
               
       [HttpPost]
       public IActionResult missionMain()
        {
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForMission();

            return PartialView("_adminMission", adminViewModelMain);
        }

        public IActionResult addMission(long missionId)
        {
            if (missionId != 0)
            { 
            
            ViewBag.missionId = missionId;  
            }
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForAddMission(missionId);

            return PartialView("_adminAddMission", adminViewModelMain);
        }

        [HttpPost]
        public IActionResult AddMissionWithDetails(AdminViewModel adminViewModelMain,long[] Skilllist, List<IFormFile> defaultImage, List<IFormFile> missionImages, string MissionVideoURL, string goalObjectiveText, int goalValue)
        {
            if (Skilllist != null && defaultImage != null && MissionVideoURL != null && adminViewModelMain.singleMission.MissionType.ToLower() == "goal")
            {

                _adminRepository.adminViewModelMainForAddMissionDetails(adminViewModelMain, Skilllist, defaultImage, missionImages, MissionVideoURL, goalObjectiveText, goalValue);
            }
            else if (Skilllist != null && defaultImage != null && missionImages != null && MissionVideoURL != null && adminViewModelMain.singleMission.MissionType.ToLower() == "time")
            {
                _adminRepository.adminViewModelMainForAddMissionDetails(adminViewModelMain, Skilllist, defaultImage, missionImages, MissionVideoURL, null , 0);

            }

            AdminViewModel adminViewModelMainAdd = _adminRepository.adminViewModelMainForMission();
            return RedirectToAction("AdminPage");
                       
        }        

        [HttpPost]
        public IActionResult editMission(long missionId)
        {
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForEditMission(missionId);

            return PartialView("_adminAddMission", adminViewModelMain);
        }

        
        [HttpPost]
        public IActionResult deleteMission(long missionId)
        {
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForDeleteMission(missionId);

            return PartialView("_adminAddMission", adminViewModelMain);
        }
                
        
        [HttpPost]
        public IActionResult otherMissionEditDetails(long missionId)
        {
            
            if (_cidatabaseContext.MissionMedia.FirstOrDefault(m => m.MissionId == missionId && m.MediaType == "URL")!=null)
            {

                var videoLink = _cidatabaseContext.MissionMedia.FirstOrDefault(m => m.MissionId == missionId && m.MediaType == "URL").MediaPath;

                var skills = _cidatabaseContext.MissionSkills.Where(m => m.MissionId == missionId).ToList();

                long[] myMissionSkills = new long[50];
                int index = 0;
                foreach (var i in skills)
                {
                    myMissionSkills[index] = (i.SkillId);
                    index++;
                }

                var myObject = new
                {
                    videoLink = videoLink,
                    myMissionSkills = myMissionSkills,

                };

                return Json(myObject);
            }
            else {
                return Json(null);
           }
               
            
        }

        
        [HttpPost]
        public IActionResult otherMissionImages(long missionId)
        {

            if (_cidatabaseContext.MissionMedia.FirstOrDefault(m => m.MissionId == missionId && m.MediaType != "URL") != null)
            {              

                IEnumerable<string> paths = _cidatabaseContext.MissionMedia.Where(m => m.MissionId == missionId).Select(m => m.MediaPath).ToList();


                return Json(paths);
            }
            else
            {
                return Json(null);
            }


        }



        // For Banner
        

       [HttpPost]
       public IActionResult bannerMain()
        {
            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForBanner();

            return PartialView("_adminBannerManagement", adminViewModelMain);
        }


        [HttpPost]
        public IActionResult AdminAddBanner()
        {
            var bannerText = Request.Form.Where(x => x.Key == "bannerText").FirstOrDefault().Value;
            string sort = Request.Form.Where(x => x.Key == "sortOrder").FirstOrDefault().Value;
            int sortOrder = int.Parse(sort);
            if (Request.Form.Files.Count() > 0)
            {
                IFormFile file = Request.Form.Files[0];
                _adminRepository.addBanner(file, bannerText, sortOrder);
            }
                    


            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForBanner();

            return PartialView("_adminBannerManagement", adminViewModelMain);
        }

        

        [HttpPost]
        public IActionResult bannerDetail(long bannerId)
        {

            var banner = _cidatabaseContext.Banners.Where(b => b.BannerId == bannerId).FirstOrDefault();
            

            var myObject = new
            {
                bannerId= banner.BannerId,
                bannerText = banner.Text,
                sortOrder = banner.SortOrder,
                bannerImage=banner.Image,

            };

            return Json(myObject);
        }

        

        [HttpPost]
        public IActionResult AdminEditBanner()
        {
            var bannerTextEdit = Request.Form.Where(x => x.Key == "bannerTextEdit").FirstOrDefault().Value;
            string sortOrder = Request.Form.Where(x => x.Key == "sortOrderEdit").FirstOrDefault().Value;
            string bannerIdWait= Request.Form.Where(x => x.Key == "bannerId").FirstOrDefault().Value;
            long bannerId= Convert.ToInt64(bannerIdWait);
            int sortOrderEdit = int.Parse(sortOrder);
            if (Request.Form.Files.Count() > 0)
            {
                IFormFile file = Request.Form.Files[0];
                _adminRepository.addEditBanner(bannerId,file, bannerTextEdit, sortOrderEdit);
            }



            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForBanner();

            return PartialView("_adminBannerManagement", adminViewModelMain);
        }


        [HttpPost]
        public IActionResult bannerDelete(long bannerId)
        {
            _adminRepository.bannerDelete(bannerId);


            AdminViewModel adminViewModelMain = _adminRepository.adminViewModelMainForBanner();

            return PartialView("_adminBannerManagement", adminViewModelMain);

        }

    }
}
