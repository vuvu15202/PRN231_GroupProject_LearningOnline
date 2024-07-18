using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        [HttpGet("userprofile")]
        public IActionResult UserProfile()
        {
            return View("UserProfile");
        }
    }
}
