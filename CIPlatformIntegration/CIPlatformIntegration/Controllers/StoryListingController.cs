﻿using CIPlatformIntegration.Entities.Data;
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
            var userIdForStoryAdd = (long)HttpContext.Session.GetInt32("farfavuserid");

            ViewData["appliedMissions"] = _cidatabaseContext.MissionApplications.Where(x => x.UserId == userIdForStoryAdd).ToList();
            ViewBag.profilename = HttpContext.Session.GetString("profile");
            ViewData["missions"] = _cidatabaseContext.Missions.ToList();
            return View();
        }

        
        
        public JsonResult StoryByDraft(int missionIdSelected)
        {
            Draft draft = new Draft();

            var userIdForStoryAdd = (long)HttpContext.Session.GetInt32("farfavuserid");

            var draftCheck = _cidatabaseContext.Stories.Where(s => s.MissionId == missionIdSelected && s.UserId == userIdForStoryAdd).FirstOrDefault();
            var storyMedium = _cidatabaseContext.StoryMedia.ToList();


            if (draftCheck != null)
            {

                draft.title = draftCheck.Title;
                draft.description = draftCheck.Description;
                draft.path = storyMedium.Where(m => m.StoryId == draftCheck.StoryId).Select(m => m.Path).FirstOrDefault();
                draft.date = draftCheck.PublishedAt.ToString();


                var draftDetails = new { title = draft.title, description = draft.description, path= draft.path, date= draft.date };

                return Json(draftDetails);
            }
            else
            {

                return null;

            }



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
            model.Status = "DRAFT";
            model.PublishedAt = convertedDate;


            _cidatabaseContext.Stories.Add(model);

            _cidatabaseContext.SaveChanges();


            long story_id = model.StoryId;

            if (formFile.Count > 0)
            {
                foreach (var file in formFile)
                {

                    string fileName = Path.GetFileName(file.FileName);



                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                    string ImageURL = "\\images\\" + fileName;


                    HttpContext.Session.SetString("uploadpath", ImageURL);

                    var stream = new FileStream(uploadpath, FileMode.Create);

                    file.CopyToAsync(stream);



                    StoryMedia(story_id);
                }
            }





            /*return View(model);*/



            return RedirectToAction("StoryListingPage", "StoryListing");


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

            StoryDetailViewModel storyDetailViewModel = new StoryDetailViewModel();

            var userID = _cidatabaseContext.Stories.FirstOrDefault(s => s.StoryId == storyid).UserId;
            storyDetailViewModel.Users = _cidatabaseContext.Users.Where(u => u.UserId == userID).ToList();

            var missionID = _cidatabaseContext.Stories.FirstOrDefault(s => s.StoryId == storyid).MissionId;
            storyDetailViewModel.Missions = _cidatabaseContext.Missions.Where(m => m.MissionId == missionID).ToList();

            storyDetailViewModel.Stories = _cidatabaseContext.Stories.Where(s => s.StoryId == storyid).ToList();

            storyDetailViewModel.storyMedia = _cidatabaseContext.StoryMedia.Where(sm => sm.StoryId == storyid).ToList();

            return View(storyDetailViewModel);
        }
    }
}
