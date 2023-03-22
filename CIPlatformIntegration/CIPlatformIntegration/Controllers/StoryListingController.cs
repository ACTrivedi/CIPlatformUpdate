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


        public StoryListingController(ILogger<StoryListingController> logger,CIDatabaseContext cIDatabaseContext)
        {
            _logger = logger;
            _cidatabaseContext= cIDatabaseContext;

        }



        public IActionResult StoryAddingPage()
        {

            HomePageViewModel model = new HomePageViewModel();

            model.Missions = _cidatabaseContext.Missions.ToList();
            model.Users = _cidatabaseContext.Users.ToList();


            return View(model);        
        }
    }
}
