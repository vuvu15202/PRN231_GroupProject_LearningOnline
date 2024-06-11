using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Controllers
{
    [Authorize]
    public class CoursesController : Controller
	{
        DonationWebApp_v2Context _context;

        public CoursesController(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        [Authorize(RoleEnum.Student)]
        public async Task<IActionResult> lesson(int? courseId, int? lessonNum = 1)
		{
            if (courseId == null)
            {
                return Redirect("/Error/Error404");
            }
            var courseInfo = await _context.Courses.FindAsync(courseId);
            if (courseInfo == null)
            {
                return Redirect($"/Courses/payment?courseId={courseId}&statuscode=4");
            }


            var user = (User)HttpContext.Items["User"];
            var courseEnroll = await _context.CourseEnrolls
                .Where(ce => ce.CourseId == courseId && ce.UserId == user.UserId).FirstOrDefaultAsync();
            var lessonInfo = await _context.Lessons
                .Where(l => l.LessonNum == lessonNum && l.CourseId == courseId).FirstOrDefaultAsync();
            if(courseEnroll != null)
            {
                if (courseEnroll.LessonCurrent < lessonNum)
                {
                    return Redirect($"/Courses/lesson?courseId={courseId}&lessonNum={courseEnroll.LessonCurrent}");
                }
            }
            else
            {
                if (courseInfo.Price == 0)
                {
                    CourseEnroll newCourseEnroll = new CourseEnroll()
                    {
                        UserId = user.UserId,
                        CourseId = (int)courseId,
                        EnrollDate = DateTime.Now,
                        LessonCurrent = 1,
                        CourseStatus = 1,
                    };
                    _context.CourseEnrolls.Add(newCourseEnroll);
                    _context.SaveChanges();
                }
                else
                {
                    return Redirect($"/Courses/payment?courseId={courseId}&statuscode=3");
                }
                
                
            }
            return View();
		}

        public IActionResult Payment()
        {
            return View();
        }
    }
    
}
