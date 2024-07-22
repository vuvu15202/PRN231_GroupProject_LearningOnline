using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        // GET: AdminController
        [HttpGet("billings")]
        public ActionResult Billing()
        {
            var user = HttpContext.Items["User"] as User; 
            ViewBag.User = user;
            return View();
        }


        [HttpGet("projects")]
        [Authorize(RoleEnum.Admin)]
        public ActionResult ListProjectAdmin()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View("Project");
        }



        [HttpGet("dashboard")]
        [Authorize(RoleEnum.Admin)]
        public ActionResult Dashboard()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View("Dashboard");
        }

        [HttpGet("studentfee")]
        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        public ActionResult StudentFee()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View("StudentFee");
        }

        [HttpGet("Notification")]
        [Authorize(RoleEnum.Admin, RoleEnum.Staff)]
        public ActionResult Notification()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View("Notification");
        }

        [HttpGet("Temp")]
        public ActionResult temp()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View("Temp");
        }

        [Authorize(RoleEnum.Lecturer)]
        [HttpGet("Courses")]
        public ActionResult Courses()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View("Courses");
        }

        [Authorize(RoleEnum.Lecturer)]
        [HttpGet("Lessons")]
        public ActionResult Lessons()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View("Lessons");
        }

        [Authorize(RoleEnum.Admin,RoleEnum.Lecturer)]
        [HttpGet("Categories")]
        public ActionResult Categories()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View();
        }


        //// GET: AdminController/Details/5
        //[HttpGet]
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: AdminController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AdminController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AdminController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: AdminController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: AdminController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AdminController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [Authorize(RoleEnum.Admin, RoleEnum.Staff, RoleEnum.Lecturer)]
        [HttpGet("usermanagement")]
        public IActionResult UserManagement()
        {
            var user = HttpContext.Items["User"] as User;
            ViewBag.User = user;
            return View("UserManagement");
        }
    }
}
