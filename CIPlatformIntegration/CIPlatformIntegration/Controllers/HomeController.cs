using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using User =CIPlatformIntegration.Entities.Models.User;

using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Mail;
using CIPlatformIntegration.Models;
using NuGet.Common;
using Microsoft.EntityFrameworkCore.Storage;

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

         
            var status = _cidatabaseContext.Users.Where(m=>m.Email == _user.Email && m.Password == _user.Password).FirstOrDefault();
            if (status == null)
            {
                ViewBag.loginstatus = 0;
            }
           
            else {


                HttpContext.Session.SetString("profile", status.FirstName);
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

            /* var userDetails = new User()
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
             return RedirectToAction("Login", "Home");*/
            User data = new User();
            data.Email = user.Email;
            data.Password = user.Password;
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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

                // Store the token in the password resets table with the user's email
                var PasswordResetdetails = new Entities.Models.PasswordReset
                {
                    Email = _user.Email,
                    Token = token
                };


            //HttpSession for ForgetPassword
            HttpContext.Session.SetString("Token", token);
          

            _cidatabaseContext.PasswordResets.Add(PasswordResetdetails);
                _cidatabaseContext.SaveChanges();

                // Send an email with the password reset link to the user's email address
                var resetLink = Url.Action("Resetpassword", "Home", new { email = _user.Email, token }, Request.Scheme);

                // Send email to user with reset password link
                // ...
                var fromAddress = new MailAddress("agarichintan2002@gmail.com");
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
                smtp.Credentials = new NetworkCredential("mailto:agarichintan2002@gmail.com", "tdbibtboqrwrcahc");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }

          

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

        public IActionResult Resetpassword(PasswordReset PReset)
        {
            var token = HttpContext.Session.GetString("Token");
          
            var validateuser = _cidatabaseContext.PasswordResets.FirstOrDefault(m => m.Token == token);     
            if (validateuser != null)
            {
                var userdata = _cidatabaseContext.Users.Where(m => m.Email == validateuser.Email).FirstOrDefault();
                userdata.Password = PReset.Password;

               
                    _cidatabaseContext.Users.Update(userdata);
                    _cidatabaseContext.SaveChanges();
                    HttpContext.Session.Remove(token);
                    return RedirectToAction("Login");

                
            }
            return RedirectToAction("Index");
        }


        // For Resetpassword Ends



        // For Homepage start


        [HttpGet]
        public IActionResult Homepage()
        {
            ViewBag.profilename=HttpContext.Session.GetString("profile");
            return View();
        }

        [HttpPost]
        /* public IActionResult Homepage(User _user)
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

        // For Homepage ends







        // For VolunteerMissionPage start


        [HttpGet]
        public IActionResult VolunteeringMissionPage()
        {


            return View();
        }

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


            return View();
        }

        [HttpPost]
        /* public IActionResult StoryListingPage(User _user)
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