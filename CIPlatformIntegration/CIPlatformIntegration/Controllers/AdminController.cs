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

            var userIdForUserEdit = (long)HttpContext.Session.GetInt32("farfavuserid");
            var userUpdate = _cidatabaseContext.Users.Where(u => u.UserId == userIdForUserEdit).FirstOrDefault();

            AdminViewModel adminViewModel = new AdminViewModel
            {

                countries = _cidatabaseContext.Countries.ToList(),
                cities = _cidatabaseContext.Cities.Where(c => c.CityId == userUpdate.CityId).ToList(),
                User = _cidatabaseContext.Users.FirstOrDefault(u=>u.UserId==userIdForUserEdit),

                users=_cidatabaseContext.Users.ToList(),
            };
            return View(adminViewModel);
        }

        [HttpPost]
        public IActionResult AdminPage(int something)
        {


            return View();
        }

        [HttpPost]
        public IActionResult AdminAddUser(int countryid, int cityid, string name, string surname, string email, string password, IFormFile avatar,string employee_id, string department, string profile_text)
        {


            User user = new User { 
                FirstName=name,
                LastName=surname,
                Email=email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                         
                EmployeeId=employee_id,
                Department=department,
                ProfileText=profile_text,
                CityId=cityid,
                CountryId=countryid,
                
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

        //For City
        [HttpPost]
        public JsonResult GetCitiesByCountryId(int Country_id)
        {
            var cities = _cidatabaseContext.Cities.Where(c => c.CountryId == Country_id).ToList();

            return Json(cities);
        }



        [HttpPost]

        public IActionResult userEdit(int selectedUserId)
        {

            var userSearch = from user in _cidatabaseContext.Users
                             join city in _cidatabaseContext.Cities
                             on user.CityId equals city.CityId
                             where user.UserId == selectedUserId
                             select new
                             { // result selector 
                                 Firstname = user.FirstName,
                                 Lastname = user.LastName,
                                 Email = user.Email,
                                 Phonenumber = user.PhoneNumber,
                                 avatar = user.Avatar,
                                 manager= user.Manager,
                                 avalibility =user.Availability,
                                 employee_id= user.EmployeeId,
                                 department= user.Department,
                                 country_id = user.CountryId,
                                 city_id= user.CityId,
                                 profile_text = user.ProfileText,
                                 status = user.Status, 
                                 city_name= city.Name,
                              
                           };

            return Json(userSearch);
            
        
        }






    }
}
