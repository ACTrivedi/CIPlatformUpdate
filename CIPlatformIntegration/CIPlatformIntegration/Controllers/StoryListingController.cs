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

            ViewBag.profilename = HttpContext.Session.GetString("profile");

            var model = _cidatabaseContext.Stories.ToList();

            return View(model);
           
        }

        // For StoryListingPage ends



        public IActionResult _CardsStoryListing(int pg)
        {


            ViewData["countries"] = _cidatabaseContext.Countries.ToList();
            ViewData["cities"] = _cidatabaseContext.Cities.ToList();
            ViewData["themes"] = _cidatabaseContext.MissionThemes.ToList();
            ViewData["skills"] = _cidatabaseContext.Skills.ToList();
            ViewData["users"] = _cidatabaseContext.Users.ToList();
            //Extra Code for the Pagination

            const int pageSize = 9;
            if (pg < 1)
                pg = 1;

            var model = _cidatabaseContext.Stories.ToList();

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

            ViewBag.profilename = HttpContext.Session.GetString("profile");
            ViewData["missions"] = _cidatabaseContext.Missions.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult StoryAddingPageCall(IFormFile formFile,string title, string postingdate, string textarea, int selectedFromDropdown)
        {



            string fileName = Path.GetFileName(formFile.FileName);

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

            

            HttpContext.Session.SetString("uploadpath", uploadpath);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);

            

            ViewData["missions"] = _cidatabaseContext.Missions.ToList();



            var missionIdForStoryAdd = selectedFromDropdown;
            var userIdForStoryAdd = long.Parse(HttpContext.Session.GetString("farfavuserid"));
            
            var convertedDate = Convert.ToDateTime(postingdate);


            Story model = new Story();

            model.UserId = userIdForStoryAdd;
            model.MissionId = missionIdForStoryAdd;
            model.Title = title;
            model.Description = textarea;
            model.Status = "DRAFT";
            

            _cidatabaseContext.Stories.Add(model);
           
            _cidatabaseContext.SaveChanges();


            /*return View(model);*/

            long story_id = model.StoryId;

            StoryMedia(story_id);

            return RedirectToAction("StoryListingPage","StoryListing");

            
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
    }
}
