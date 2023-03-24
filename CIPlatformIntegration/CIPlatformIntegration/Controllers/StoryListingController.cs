using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult StoryListingPage()
        {

            ViewData["countries"] = _cidatabaseContext.Countries.ToList();
            ViewData["cities"] = _cidatabaseContext.Cities.ToList();
            ViewData["themes"] = _cidatabaseContext.MissionThemes.ToList();
            ViewData["skills"] = _cidatabaseContext.Skills.ToList();
            ViewData["users"] = _cidatabaseContext.Users.ToList();

            var model = _cidatabaseContext.Stories.ToList();
            return View(model);
           
        }

        // For StoryListingPage ends




        public IActionResult StoryAddingPage()
        {
           

            ViewData["missions"] = _cidatabaseContext.Missions.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult StoryAddingPageCall(IFormFile formFile,string title, string postingdate, string textarea, int selectedFromDropdown)
        {



            string fileName = Path.GetFileName(formFile.FileName);

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);

            





            ViewData["missions"] = _cidatabaseContext.Missions.ToList();

            var missionIdForStoryAdd = selectedFromDropdown;
            var userIdForStoryAdd = long.Parse(HttpContext.Session.GetString("farfavuserid"));
            
            var convertedDate = Convert.ToDateTime(postingdate);


            Story model = new Story();

            model.UserId = userIdForStoryAdd;
            model.MissionId = 1;
            model.Title = title;
            model.Description = textarea;
            model.Status = "DRAFT";

            StoryMedium storyMedium = new StoryMedium();
            storyMedium.Path = uploadpath;
            storyMedium.StoryId = 10;
            storyMedium.Type = ".jfif";
            _cidatabaseContext.StoryMedia.Add(storyMedium);
            _cidatabaseContext.Stories.Add(model);
            _cidatabaseContext.SaveChanges();

            return View(model);
        }
    }
}
