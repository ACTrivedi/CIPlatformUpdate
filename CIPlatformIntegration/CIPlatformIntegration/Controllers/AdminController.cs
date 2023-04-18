using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;


namespace CIPlatformIntegration.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        //CIDatabaseContext _cidatabaseContext = new CIDatabaseContext();
        private readonly CidatabaseContext _cidatabaseContext;


        public AdminController(ILogger<AdminController> logger, CidatabaseContext cIDatabaseContext)
        {
            _logger = logger;
            _cidatabaseContext = cIDatabaseContext;

        }

        [HttpGet]
        public IActionResult AdminPage()
        {
            AdminViewModel adminViewModel = new AdminViewModel
            {
                users = _cidatabaseContext.Users.ToList(),
            };
            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult AdminPage(int something)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminAddUser(string name, string surname, string email, string password, IFormFile avatar,string employee_id, string department, string profile_text)
        {


            User user = new User { 
                FirstName=name,
                LastName=surname,
                Email=email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                         
                EmployeeId=employee_id,
                Department=department,
                ProfileText=profile_text
            };
            if (avatar != null)
            {
                string imageURL = "\\images\\" + Path.GetFileName(avatar.FileName);                
                user.Avatar = imageURL;

            }

            _cidatabaseContext.Users.Add(user);
            _cidatabaseContext.SaveChanges();
                                    
            return RedirectToAction("AdminPage");
        }






    }
}
