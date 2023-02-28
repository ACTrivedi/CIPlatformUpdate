using CIPlatformIntegration.Entities.Data;
using CIPlatformIntegration.Entities.Models;
using CIPlatformIntegration.Models;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CIPlatformIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


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

            CidatabaseContext _cidatabaseContext = new CidatabaseContext();
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
        public IActionResult Forgotpassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Forgotpassword(User _user)
        {

            CidatabaseContext _cidatabaseContext = new CidatabaseContext();
            var status = _cidatabaseContext.Users.Where(m => m.Email == _user.Email && m.Password == _user.Password).FirstOrDefault();
            if (status == null)
            {
                ViewBag.loginstatus = 0;
            }
            else
            {
                return RedirectToAction("Homepage", "Home");
            }
            return View(_user);
        }

        //For Forgotpassword Ends


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