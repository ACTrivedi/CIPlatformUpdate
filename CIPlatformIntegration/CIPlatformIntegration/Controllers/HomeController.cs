using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using User = CIPlatformIntegration.Entities.Models.User;

using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Mail;
using CIPlatformIntegration.Models;
using NuGet.Common;
using Microsoft.EntityFrameworkCore.Storage;
using CIPlatformIntegration.Entities.ViewModel;
using MySqlX.XDevAPI;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Numerics;
using Org.BouncyCastle.Crypto.Tls;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Tracing;

namespace CIPlatformIntegration.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        CIDatabaseContext _cidatabaseContext = new CIDatabaseContext();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public IActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public IActionResult Login(User _user)
        {
            var emailstatus = _cidatabaseContext.Users.FirstOrDefault(m => m.Email == _user.Email);

            HttpContext.Session.SetString("farfavuserid", emailstatus.UserId.ToString());

            bool verified = BCrypt.Net.BCrypt.Verify(_user.Password, emailstatus.Password);
            var status = _cidatabaseContext.Users.Where(m => m.Email == _user.Email && verified).FirstOrDefault();
            if (status == null)
            {
                ViewBag.loginstatus = 0;
            }
            else
            {
                TempData["Toastlogin"] = "Login Successfull";

             /*   HttpContext.Session.SetString("Loggedin", _user.Email);*/

                /* HttpContext.Session.SetString("Loggedin", "True");*/
                HttpContext.Session.SetString("profile", status.FirstName);
                HttpContext.Session.SetString("Loggedin", _user.Email);
                return RedirectToAction("Homepage", "Home");
            }

            return View(_user);

        }


        // For Registration start

        [HttpGet]
        public IActionResult Registration()
        {


            return View();
        }


       


       [HttpPost]
        public IActionResult Registration(User user)
        {

            User data = new User();
            data.Email = user.Email;
            data.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            data.PhoneNumber = user.PhoneNumber;
            data.CityId = 1;
            data.CountryId = 1;
            data.LastName = user.LastName;
            data.FirstName = user.FirstName;


            if (ModelState.IsValid)
            {

                _cidatabaseContext.Users.Add(data);
                _cidatabaseContext.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }

        // For Registration ends


        //For Forgotpassword start

        [HttpGet]
        public IActionResult Forgotpassword()
        {
            return View();
        }
        [HttpPost]


        public ActionResult Forgotpassword(User _user)
        {

            var useremailverify = _cidatabaseContext.Users.FirstOrDefault(u => u.Email == _user.Email);
            if (useremailverify == null)
            {
                /*return RedirectToAction("Index", "Home");*/
                ViewBag.forgetstatus = 0;


            }

            // Generate a password reset token for the user
            var token = Guid.NewGuid().ToString();

            HttpContext.Session.SetString("Emailpassing", _user.Email);
            TempData["email"] = useremailverify.Email;

            // Store the token in the password resets table with the user's email
            var PasswordResetdetails = new Entities.Models.PasswordReset
            {
                Email = _user.Email,
                Token = token
            };


            //HttpSession for ForgetPassword





            _cidatabaseContext.PasswordResets.Add(PasswordResetdetails);
            _cidatabaseContext.SaveChanges();

            // Send an email with the password reset link to the user's email address
            var resetLink = Url.Action("Resetpassword", "Home", new { email = _user.Email, token }, Request.Scheme);

            // Send email to user with reset password link
            // ...
            var fromAddress = new MailAddress("vasudedakiya3@gmail.com");
            var toAddress = new MailAddress(_user.Email);
            var subject = "Password reset request";
            var body = $"Hi,<br /><br />Please click on the following link to reset your password:<br /><br /><a href='{resetLink}'>{resetLink}</a>";
            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("aksharpatel18092000@gmail.com", "gptnvpkcimqkuktp");

                smtp.EnableSsl = true;
                smtp.Send(message);
            }

            HttpContext.Session.SetString("Token", token);



            /*return RedirectToAction("", "Home");*/


            return View();


        }

        //For Forgotpassword ends


        //For Resetpassword starts


        [HttpGet]
        public IActionResult Resetpassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Resetpassword(PasswordReset PReset)
        {
            var token = HttpContext.Session.GetString("Token");

            var validateuser = _cidatabaseContext.PasswordResets.FirstOrDefault(m => m.Email == PReset.Email);
            if (validateuser == null)
            {
                return RedirectToAction("Index");
            }


            var userdata = _cidatabaseContext.Users.Where(m => m.Email == validateuser.Email).FirstOrDefault();
            userdata.Password = PReset.Password;


            _cidatabaseContext.Users.Update(userdata);
            _cidatabaseContext.SaveChanges();

            return RedirectToAction("Login");




        }

        // For Resetpassword Ends


        // For Homepage start

        public IActionResult Homepage(string Cardsearch, int pg = 1, int id = 0)
        {
            var verifieduser = HttpContext.Session.GetString("Loggedin");
            if (verifieduser == null)
            {
                return RedirectToAction("Login", "Home");
            }


            ViewData["countries"] = _cidatabaseContext.Countries.ToList();
            ViewData["cities"] = _cidatabaseContext.Cities.ToList();
            ViewData["themes"] = _cidatabaseContext.MissionThemes.ToList();
            ViewData["skills"] = _cidatabaseContext.Skills.ToList();
            ViewData["goalMission"] = _cidatabaseContext.GoalMissions.ToList();

            ViewBag.profilename = HttpContext.Session.GetString("profile");


            IEnumerable<Mission> missionobj = _cidatabaseContext.Missions.ToList();

            switch (id)
            {
                case 1:
                    missionobj = _cidatabaseContext.Missions.OrderBy(p => p.StartDate).ToList();
                    break;
                case 2:
                    missionobj = _cidatabaseContext.Missions.OrderBy(p => p.EndDate).ToList();
                    break;
                case 3:
                    missionobj = _cidatabaseContext.Missions.OrderBy(p => p.CreatedAt).ToList();
                    break;
            }

            foreach (var mission1 in missionobj)
            {
                _cidatabaseContext.Entry(mission1).Reference(c => c.City).Load();
                _cidatabaseContext.Entry(mission1).Reference(c => c.Country).Load();
                _cidatabaseContext.Entry(mission1).Reference(t => t.Theme).Load();

            }

            if (!String.IsNullOrEmpty(Cardsearch))
            {

                return View(missionobj.Where(x => x.Theme.Title == Cardsearch));
            }

            //Extra Code for the Pagination

            const int pageSize = 9;
            if (pg < 1)
                pg = 1;

            int recsCount = missionobj.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = missionobj.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            missionobj = data.ToList();

            return View(missionobj);

            /* HttpContext.Session.Remove("Loggedin");*/


        }

        // For Homepage ends


        // For VolunteerMissionPage start


        [HttpGet]
        public IActionResult VolunteeringMissionPage(int missionid)
        {


            //Start Session for passing missionID to StarRating
            HttpContext.Session.SetInt32("starmissionid", missionid);
            //End Session for passing missionID to StarRating





            ViewData["countries"] = _cidatabaseContext.Countries.ToList();
            ViewData["cities"] = _cidatabaseContext.Cities.ToList();
            ViewData["themes"] = _cidatabaseContext.MissionThemes.ToList();
            ViewData["skills"] = _cidatabaseContext.Skills.ToList();
            ViewData["goalMission"] = _cidatabaseContext.GoalMissions.ToList();
            ViewData["favMission"] = _cidatabaseContext.FavoriteMissions.ToList();
            ViewData["comments"] = _cidatabaseContext.Comments.Where(c=>c.MissionId==missionid).ToList();
            ViewData["useridcheck"] = long.Parse(HttpContext.Session.GetString("farfavuserid"));
          
           


            IEnumerable<Mission> missionobj1 = _cidatabaseContext.Missions.Where(m => m.MissionId == missionid);
            

            return View(missionobj1);
            


        }
        /*STar RAting*/
        public IActionResult StarRating(int s)
        {
            var missionId=HttpContext.Session.GetInt32("starmissionid");
            var userId = long.Parse(HttpContext.Session.GetString("farfavuserid"));
            var star = new MissionRating()
            {
                UserId = userId,
                MissionId = (long)missionId,
                Rating = s
            };
            _cidatabaseContext.MissionRatings.Add(star);
            _cidatabaseContext.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        /*sTar Rating ends */

        [HttpPost]
        /* public IActionResult VolunteeringMissionPage(User _user)
         {

             var userDetails = new User()
             {
                 FirstName = _user.FirstName,
                 LastName = _user.LastName,
                 PhoneNumber = _user.PhoneNumber,
                 Email = _user.Email,
                 Password = _user.Password,
                 CityId = 4,
                 CountryId = 4
             };
             _cidatabaseContext.Users.Add(userDetails);
             _cidatabaseContext.SaveChanges();
             return RedirectToAction("Login", "Home");

         }*/

        // For VolunteeringMissionPage ends




        // For StoryListingPage start


        [HttpGet]
        public IActionResult StoryListingPage()
        {

            ViewData["countries"] = _cidatabaseContext.Countries.ToList();
            ViewData["cities"] = _cidatabaseContext.Cities.ToList();
            ViewData["themes"] = _cidatabaseContext.MissionThemes.ToList();
            ViewData["skills"] = _cidatabaseContext.Skills.ToList();



            return View();
        }

        // For StoryListingPage ends




        [HttpGet]
        public JsonResult Country()
        {
            var cnt = _cidatabaseContext.Countries.ToList();
            return new JsonResult(cnt);
        }



        public JsonResult City(int id)
        {
            var st = _cidatabaseContext.Cities.Where(e => e.CountryId == id).ToList();
            return new JsonResult(st);
        }
        public JsonResult Skill()
        {
            var sk = _cidatabaseContext.Skills.ToList();
            return new JsonResult(sk);
        }


        public JsonResult MissionTheme()
        {


            var themevar = _cidatabaseContext.MissionThemes.ToList();
            return new JsonResult(themevar);
        }




        public async Task<IActionResult> Toggle(int missionid)
        {

            var userid = long.Parse(HttpContext.Session.GetString("farfavuserid"));

            

            FavoriteMission favorite = await _cidatabaseContext.FavoriteMissions.FirstOrDefaultAsync(f => f.UserId == userid && f.MissionId == missionid);
            if (favorite != null)
            {
                // Remove the item from the user's favorites
                _cidatabaseContext.FavoriteMissions.Remove(favorite);
                await _cidatabaseContext.SaveChangesAsync();
            }

            else
            {
                FavoriteMission favorite2 = new FavoriteMission();
                favorite2.UserId = userid;
                favorite2.MissionId = missionid;

                _cidatabaseContext.Add(favorite2);
                _cidatabaseContext.SaveChanges();

               

            }

            return RedirectToAction("VolunteeringMissionPage", new { missionid = missionid });
        }




        [HttpPost]
        public IActionResult Createcomment(String formData) 
        {
            var missionIdforcomment = HttpContext.Session.GetInt32("starmissionid");

            var userIdforcomment= long.Parse(HttpContext.Session.GetString("farfavuserid"));

            var commenttable = new Comment()
            {
                UserId = userIdforcomment,
                MissionId = (long)missionIdforcomment,
                ApprovalStatus = "Published",
                CommentText = formData

            };
 
            _cidatabaseContext.Add(commenttable);
            _cidatabaseContext.SaveChanges();



            return Json(new { success = true });
        }


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}