using Microsoft.AspNetCore.Mvc;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Notification()
        {
            return View();
        }
    }
}
