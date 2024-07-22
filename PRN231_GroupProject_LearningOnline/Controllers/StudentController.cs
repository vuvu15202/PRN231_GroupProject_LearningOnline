using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Models.Entity;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Enroll()
        {
			var user = HttpContext.Items["User"] as User;
			ViewBag.User = user;
			return View();
        }
    }
}
