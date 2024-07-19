using Microsoft.AspNetCore.Mvc;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Enroll()
        {
            return View();
        }
    }
}
