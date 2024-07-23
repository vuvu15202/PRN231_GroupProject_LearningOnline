using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer, RoleEnum.Student)]
        [HttpGet("userprofile")]
        public IActionResult UserProfile()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View(user);
        }
    }
}
