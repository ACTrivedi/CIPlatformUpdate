using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text;

namespace CIPlatformIntegration.Controllers
{
    public class StoryListingController : Controller

    {

        private readonly ILogger<StoryListingController> _logger;
        //CIDatabaseContext _cidatabaseContext = new CIDatabaseContext();
        private readonly CIDatabaseContext _cidatabaseContext;


        public StoryListingController(ILogger<StoryListingController> logger, CIDatabaseContext cIDatabaseContext)
        {
            _logger = logger;
            _cidatabaseContext = cIDatabaseContext;

        }


        // For StoryListingPage start


        [HttpGet]
        public IActionResult StoryListingPage(int pg = 1)
        {

            ViewData["countries"] = _cidatabaseContext.Countries.ToList();
            ViewData["cities"] = _cidatabaseContext.Cities.ToList();
            ViewData["themes"] = _cidatabaseContext.MissionThemes.ToList();
            ViewData["skills"] = _cidatabaseContext.Skills.ToList();
            ViewData["users"] = _cidatabaseContext.Users.ToList();
            ViewData["storyMedium"] = _cidatabaseContext.StoryMedia.ToList();

            ViewBag.profilename = HttpContext.Session.GetString("profile");

            var model = _cidatabaseContext.Stories.Where(s => s.Status == "APPROVED").ToList();

            return View(model);



        }

        // For StoryListingPage ends



        public IActionResult _CardsStoryListing(int pg, string? searchTerm)
        {


            ViewData["countries"] = _cidatabaseContext.Countries.ToList();
            ViewData["cities"] = _cidatabaseContext.Cities.ToList();
            ViewData["themes"] = _cidatabaseContext.MissionThemes.ToList();
            ViewData["skills"] = _cidatabaseContext.Skills.ToList();
            ViewData["users"] = _cidatabaseContext.Users.ToList();
            ViewData["storyMedium"] = _cidatabaseContext.StoryMedia.ToList();

            //Extra Code for the Pagination

            const int pageSize = 9;
            if (pg < 1)
                pg = 1;


            var model = _cidatabaseContext.Stories.Where(s => s.Status == "APPROVED" || s.Title.ToLower().Contains(searchTerm)).ToList();
            if (searchTerm == null)
            {
                model = _cidatabaseContext.Stories.Where(s => s.Status == "APPROVED" || s.Title.ToLower().Contains(searchTerm)).ToList();
            }
            else
            {
                model = _cidatabaseContext.Stories.Where(s => s.Status == "APPROVED" && s.Title.ToLower().Contains(searchTerm)).ToList();
            }



            int recsCount = model.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = model.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            model = data;

            return PartialView("_CardsStoryListing", model);


        }




        public IActionResult StoryAddingPage()
        {
            var userIdForStoryAdd = (long)HttpContext.Session.GetInt32("farfavuserid");

            ViewData["appliedMissions"] = _cidatabaseContext.MissionApplications.Where(x => x.UserId == userIdForStoryAdd).ToList();
            ViewBag.profilename = HttpContext.Session.GetString("profile");
            ViewData["missions"] = _cidatabaseContext.Missions.ToList();
            return View();
        }


        public IActionResult DraftDecide(int missionIdSelected)
        {

            var userIdForStoryAdd = (long)HttpContext.Session.GetInt32("farfavuserid");

            var storyCheck = _cidatabaseContext.Stories.Where(s => s.UserId == userIdForStoryAdd && s.MissionId == missionIdSelected && s.Status == "DRAFT").ToList();
            if (storyCheck.Count != 0)
            {
                var draftDetails = StoryByDraft(missionIdSelected);
                return Json(draftDetails);

            }
            else
            {
                return RedirectToAction("StoryAddingPage", "StoryListing");
            }

        }




        public IActionResult StoryByDraft(int missionIdSelected)
        {
            Draft draft = new Draft();

            var userIdForStoryAdd = (long)HttpContext.Session.GetInt32("farfavuserid");

            var draftCheck = _cidatabaseContext.Stories.Where(s => s.MissionId == missionIdSelected && s.UserId == userIdForStoryAdd).OrderBy(s => s.StoryId).LastOrDefault();
            var storyMedium = _cidatabaseContext.StoryMedia.ToList();
            IEnumerable<string> paths = storyMedium.Where(m => m.StoryId == draftCheck.StoryId).Select(m => m.Path).ToList();


            if (draftCheck != null)
            {

                draft.title = draftCheck.Title;
                draft.description = draftCheck.Description;
                draft.date = draftCheck.PublishedAt.ToString();
                draft.Paths = paths;






                var draftDetails = new { title = draft.title, description = draft.description, path = draft.Paths, date = draft.date };

                return Json(draftDetails);
            }
            else
            {

                return null;

            }



        }

        //For Draft
        public IActionResult StoryAddingPageCallForDraft(List<IFormFile> formFile, string title, string postingdate, string textarea, int selectedFromDropdown)
        {

            var userIdForStoryAdd = (long)HttpContext.Session.GetInt32("farfavuserid");


            var missionIdForStoryAdd = selectedFromDropdown;

            var convertedDate = Convert.ToDateTime(postingdate);


            Story model = new Story();

            if (model != null)
            {
                model.UserId = userIdForStoryAdd;
                model.MissionId = missionIdForStoryAdd;
                model.Title = title;
                model.Description = textarea;
                model.Status = "DRAFT";
                model.PublishedAt = convertedDate;
            }




            _cidatabaseContext.Stories.Add(model);

            _cidatabaseContext.SaveChanges();


            long story_id = model.StoryId;

            if (formFile.Count > 0)
            {
                foreach (var file in formFile)
                {

                    /*string fileName = Path.GetFileName(file.FileName);*/

                    FileStream FileStream = new(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\StoryImages\\", Path.GetFileName(file.FileName)), FileMode.Create);

                    /*                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\StoryImages\\", fileName);
                    */
                    string ImageURL = "\\images\\StoryImages\\" + Path.GetFileName(file.FileName);


                    HttpContext.Session.SetString("uploadpath", ImageURL);

                    /*var stream = new FileStream(uploadpath, FileMode.Create);*/

                    file.CopyToAsync(FileStream);



                    StoryMedia(story_id);

                    FileStream.Close();
                }
            }





            /*return View(model);*/



            return RedirectToAction("StoryListingPage", "StoryListing");





        }




        [HttpPost]
        public IActionResult StoryAddingPageCall(List<IFormFile> formFile, string title, string postingdate, string textarea, int selectedFromDropdown)
        {

            var userIdForStoryAdd = (long)HttpContext.Session.GetInt32("farfavuserid");


            var missionIdForStoryAdd = selectedFromDropdown;

            var convertedDate = Convert.ToDateTime(postingdate);


            Story model = new Story();

            model.UserId = userIdForStoryAdd;
            model.MissionId = missionIdForStoryAdd;
            model.Title = title;
            model.Description = textarea;
            model.Status = "APPROVED";
            model.PublishedAt = convertedDate;


            _cidatabaseContext.Stories.Add(model);

            _cidatabaseContext.SaveChanges();


            long story_id = model.StoryId;

            if (formFile.Count > 0)
            {
                foreach (var file in formFile)
                {

                    /*string fileName = Path.GetFileName(file.FileName);*/

                    FileStream FileStream = new(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\StoryImages\\", Path.GetFileName(file.FileName)), FileMode.Create);

                    /*                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\StoryImages\\", fileName);
                    */
                    string ImageURL = "\\images\\StoryImages\\" + Path.GetFileName(file.FileName);


                    HttpContext.Session.SetString("uploadpath", ImageURL);

                    /*var stream = new FileStream(uploadpath, FileMode.Create);*/

                    file.CopyToAsync(FileStream);



                    StoryMedia(story_id);


                    FileStream.Close();


                }
            }

            /*return View(model);*/

            return RedirectToAction("StoryListingPage", "StoryListing");

        }


        // Controller action that increments the view count for the current page
        public IActionResult IncrementViewCount(int count)
        {
            // Retrieve the current view count from the database

            Story story = new Story();
            story.Views = count;

            StoryDetailViewModel storyDetailViewModel = new StoryDetailViewModel();

            storyDetailViewModel.ViewCount = story.Views;



            // Return the updated view count to the client
            return Json(storyDetailViewModel.ViewCount);
        }





        public void StoryMedia(long story_id)
        {
            StoryMedium storyMedium = new StoryMedium();

            var path = HttpContext.Session.GetString("uploadpath");
            storyMedium.Path = path;

            storyMedium.StoryId = story_id;

            string ext = Path.GetExtension(path);
            storyMedium.Type = ext;

            _cidatabaseContext.StoryMedia.Add(storyMedium);
            _cidatabaseContext.SaveChanges();

        }

        public IActionResult StoryDetailPage(int storyid)
        {

            ViewBag.profilename = HttpContext.Session.GetString("profile");
            var userIdForStoryDetail = (long)HttpContext.Session.GetInt32("farfavuserid");
            HttpContext.Session.SetInt32("storyID", storyid);

            StoryDetailViewModel storyDetailViewModel = new StoryDetailViewModel();

            var userID = _cidatabaseContext.Stories.FirstOrDefault(s => s.StoryId == storyid).UserId;
            storyDetailViewModel.Users = _cidatabaseContext.Users.Where(u => u.UserId == userID).ToList();

            storyDetailViewModel.recommendUser = _cidatabaseContext.Users.ToList();


            var missionID = _cidatabaseContext.Stories.FirstOrDefault(s => s.StoryId == storyid).MissionId;
            storyDetailViewModel.Missions = _cidatabaseContext.Missions.Where(m => m.MissionId == missionID).ToList();

            storyDetailViewModel.Stories = _cidatabaseContext.Stories.Where(s => s.StoryId == storyid).ToList();

            storyDetailViewModel.storyMedia = _cidatabaseContext.StoryMedia.Where(sm => sm.StoryId == storyid).ToList();

            return View(storyDetailViewModel);
        }

        [HttpPost]
        public IActionResult Recommendtoworker(string userEmail)
        {
            var to_userID = _cidatabaseContext.Users.Where(u => u.Email == userEmail).Select(u => u.UserId).SingleOrDefault();
            var userid = (long)HttpContext.Session.GetInt32("farfavuserid");
            var missionID = HttpContext.Session.GetInt32("starmissionid");
            var storyID = (long)HttpContext.Session.GetInt32("storyID");
            if (to_userID != null)
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("ciplatformmailsender@gmail.com");
                mail.To.Add(new MailAddress(userEmail));
                mail.Subject = "Test mail";
                mail.Body = "<html><body>Click here<a href='" + "https://localhost:7296/StoryListing/StoryDetailPage?storyid=" + storyID + "'> to recommend this mission</a></body></html>";
                mail.IsBodyHtml = true;

                SmtpClient myclient = new SmtpClient();
                myclient.Host = "smtp.gmail.com";
                myclient.Port = 587;
                myclient.Credentials = new
                System.Net.NetworkCredential("ciplatformmailsender@gmail.com", "muarmclnmmtdzxqh");
                myclient.EnableSsl = true;
                myclient.Send(mail);



            }
            return RedirectToAction("StoryDetailPage", "StoryListing", new { storyid = storyID });
        }



        [HttpGet]
        public IActionResult UserEditProfile()
        {
            var userIdForUserEdit = (long)HttpContext.Session.GetInt32("farfavuserid");
            ViewBag.profilename = HttpContext.Session.GetString("profile");

            var userUpdate = _cidatabaseContext.Users.Where(u => u.UserId == userIdForUserEdit).FirstOrDefault();

            var userViewModel = new UserEditProfileViewModel();
            userViewModel.IndividualUser = userUpdate;
            userViewModel.skills = _cidatabaseContext.Skills.ToList();
            userViewModel.userSkills = _cidatabaseContext.UserSkills.Where(us => us.UserId == userIdForUserEdit).ToList();
            userViewModel.countries = _cidatabaseContext.Countries.ToList();
            userViewModel.cities = _cidatabaseContext.Cities.Where(c => c.CityId == userUpdate.CityId).FirstOrDefault();





            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult UserEditProfile(string name, string surname, string employeeID, string manager, string title, string department, string profile, string linkedInUrl, string skillsAddition, string profileText, int CountryId, int CityId)
        {

            var userIdForUserEdit = (long)HttpContext.Session.GetInt32("farfavuserid");
            ViewBag.profilename = HttpContext.Session.GetString("profile");

            if (skillsAddition != null)
            {
                string[] skillsArray = skillsAddition.Split(", ");

                foreach (string skill in skillsArray)
                {
                    var exists = _cidatabaseContext.Skills.FirstOrDefault(s => s.SkillName == skill).SkillId;
                    var userskill = _cidatabaseContext.UserSkills.FirstOrDefault(u => u.SkillId == exists);
                    if (userskill != null)
                    {
                        _cidatabaseContext.Remove(userskill);
                        _cidatabaseContext.SaveChanges();
                    }

                }
                foreach (string skill in skillsArray)
                {
                    var selectedSkills = _cidatabaseContext.Skills.FirstOrDefault(s => s.SkillName == skill).SkillId;

                    UserSkill model = new UserSkill();
                    model.UserId = userIdForUserEdit;
                    model.SkillId = selectedSkills;

                    _cidatabaseContext.UserSkills.Add(model);
                    _cidatabaseContext.SaveChanges();

                }
            }


            var userUpdate = _cidatabaseContext.Users.Where(u => u.UserId == userIdForUserEdit).FirstOrDefault();
            userUpdate.FirstName = name;
            userUpdate.LastName = surname;
            userUpdate.EmployeeId = employeeID;
            userUpdate.Title = title;
            userUpdate.Department = department;
            userUpdate.ProfileText = profileText;
            userUpdate.LinkedInUrl = linkedInUrl;

            //For Country
            if (CountryId == 0)
            {
                userUpdate.CountryId = userUpdate.CountryId;
            }
            else
            {
                userUpdate.CountryId = CountryId;
            }

            //For City
            if (CityId == 0)
            {
                userUpdate.CityId = userUpdate.CityId;
            }
            else
            {
                userUpdate.CityId = CityId;
            }



            _cidatabaseContext.Users.Update(userUpdate);
            _cidatabaseContext.SaveChanges();


            var userViewModel = new UserEditProfileViewModel();
            userViewModel.IndividualUser = userUpdate;
            userViewModel.skills = _cidatabaseContext.Skills.ToList();
            userViewModel.userSkills = _cidatabaseContext.UserSkills.Where(us => us.UserId == userIdForUserEdit).ToList();
            userViewModel.countries = _cidatabaseContext.Countries.ToList();
            userViewModel.cities = _cidatabaseContext.Cities.Where(c => c.CityId == userUpdate.CityId).FirstOrDefault();


            return View(userViewModel);

        }




        //For City
        [HttpPost]
        public JsonResult GetCitiesByCountryId(int countryId)
        {
            var cities = _cidatabaseContext.Cities.Where(c => c.CountryId == countryId).ToList();
           
            return Json(cities);
        }


        [HttpPost]
        public IActionResult UserChangePassword(string oldPassword, string newPassword, string confirmNewPassword)
        {

            var userIdForUserEdit = (long)HttpContext.Session.GetInt32("farfavuserid");
           
            var userUpdate = _cidatabaseContext.Users.Where(u => u.UserId == userIdForUserEdit).FirstOrDefault();
            

            bool verified = BCrypt.Net.BCrypt.Verify(oldPassword, userUpdate.Password);


            if (verified == true)
            {
                if (newPassword == confirmNewPassword)
                {
                    var updatedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    userUpdate.Password = updatedPassword;

                    _cidatabaseContext.Users.Update(userUpdate);
                    _cidatabaseContext.SaveChanges();

                    return Json(new { success = true });

                }
                else
                { 
                    return Json(new { success = false, message = "Your password and confirm password doesn't matched." });
                }
            }
            else
            {
                return Json(new { success = false, message = "An error occurred while doing something." });
            }
        }
    }
}
