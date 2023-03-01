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
                return RedirectToAction("Homepage","Home");
            }
            return View(_user);
        }


       





        //For Forgotpassword


        [HttpGet]
        public IActionResult Resetpassword()
        {
            return View();
        }
       




        // For Registration start


        [HttpGet]
        public IActionResult Registration()
        {


            return View();
        }

        [HttpPost]
        public IActionResult Registration(User _user)
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
                _cidatabaseContext.PasswordResets.Add(PasswordResetdetails);
                _cidatabaseContext.SaveChanges();

                // Send an email with the password reset link to the user's email address
                var resetLink = Url.Action("Resetpassword", "Home", new { email = _user.Email, token }, Request.Scheme);

                // Send email to user with reset password link
                // ...
                var fromAddress = new MailAddress("aakashtrivedi1552002@gmail.com", "Aakash");
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
                smtp.Credentials = new NetworkCredential("tatvahl@gmail.com", "dvbexvljnrhcflfw");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }

          

                /*return RedirectToAction("", "Home");*/
    

            return View();


        }

    


    //For Forgotpassword ends




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