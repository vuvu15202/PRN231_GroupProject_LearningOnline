using Microsoft.AspNetCore.Mvc;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    public class NewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
