using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Models.Entity;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View();
        }

        public IActionResult Notification()
        {
            return View();
        }

        public IActionResult Contact()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View();  
        }
    }
}
