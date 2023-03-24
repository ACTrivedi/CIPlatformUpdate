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

        public IActionResult StoryAddingPage()
        {

            ViewData["missions"] = _cidatabaseContext.Missions.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult StoryAddingPageCall(string title, string date, string storydescription,int selectedFromDropdown)
        {

            ViewData["missions"] = _cidatabaseContext.Missions.ToList();

            var missionIdForStoryAdd = selectedFromDropdown;
            var userIdForStoryAdd = long.Parse(HttpContext.Session.GetString("farfavuserid"));
            
            var convertedDate = Convert.ToDateTime(date);


            Story model = new Story();

            model.UserId = userIdForStoryAdd;
            model.MissionId = missionIdForStoryAdd;
            model.Title = title;
            model.Description = storydescription;
            model.Status = "DRAFT";


            _cidatabaseContext.Stories.Add(model);
            _cidatabaseContext.SaveChanges();

            return View(model);
        }
    }
}
