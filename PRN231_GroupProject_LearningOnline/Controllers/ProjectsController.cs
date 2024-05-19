using Microsoft.AspNetCore.Mvc;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
