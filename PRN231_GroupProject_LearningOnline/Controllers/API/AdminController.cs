using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;

namespace PRN231_GroupProject_LearningOnline.Controllers.Admin
{
    [Authorize]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        [Authorize(RoleEnum.Admin)]
        // GET: AdminController
        [HttpGet("billings")]
        public ActionResult Billing()
        {
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
        [HttpGet("projects")]
        [Authorize(RoleEnum.Admin)]
        public ActionResult ListProjectAdmin()
        {
            return View("Project");
        }

        [HttpGet("dashboard")]
        [Authorize(RoleEnum.Admin)]
        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }
    }
}
