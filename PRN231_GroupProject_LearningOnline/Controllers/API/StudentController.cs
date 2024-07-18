using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Models.Entity;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;

        public StudentController(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        [HttpGet("GetCourseByUserId")]
        public IActionResult GetCourseByUserId()
        {
            var student = (User)HttpContext.Items["User"];
            var courses = _context.CourseEnrolls.Include(ce => ce.StudentFee).Include(ce => ce.User).Include(ce => ce.Course).Where(ce => ce.UserId == student.UserId).Select(
                    ce => new CourseEnrollDTO
                    {
                        Id = ce.CourseEnrollId,
                        CourseId = ce.Course.CourseId,
                        StudentId = ce.UserId,
                        CourseName = ce.Course.Name,
                        EnrollDate = ce.EnrollDate.ToString("dd/MM/yyyy"),
                        EndDate = ce.EnrollDate.AddMonths(3).ToString("dd/MM/yyyy"),
                        CourseStatus = ce.CourseStatus,
                        AverageGrade = ce.AverageGrade,
                        StudentFeeId = ce.StudentFeeId 
                    }
                ); 
            return Ok(courses);
        }

        [HttpGet("GetCourseDetailById")]
        public IActionResult GetCourseEnrollById(int courseEnrollId)
        {
            var courses = _context.CourseEnrolls.Include(ce => ce.StudentFee).Include(ce => ce.User).Include(ce => ce.Course).Where(ce => ce.CourseEnrollId == courseEnrollId).Select(
                    ce => new CourseEnrollDTO
                    {
                        Id = ce.CourseEnrollId,
                        CourseId = ce.Course.CourseId,
                        StudentId = ce.UserId,
                        CourseName = ce.Course.Name,
                        EnrollDate = ce.EnrollDate.ToString("dd/MM/yyyy"),
                        EndDate = ce.EnrollDate.AddMonths(3).ToString("dd/MM/yyyy"),
                        CourseStatus = ce.CourseStatus,
                        AverageGrade = ce.AverageGrade,
                        StudentFeeId = ce.StudentFeeId
                    }
                );
            return Ok(courses);
        }
    }

    public class CourseEnrollDTO
    {
        public int Id { get; set; } 
        public int CourseId { get; set; }   
        public int StudentId { get; set; }
        public string CourseName { get; set; }  
        public string EnrollDate { get; set; }  
        public string EndDate { get; set; } 
        public int CourseStatus { get; set; }   
        public float? AverageGrade { get; set; }
        public string? StudentFeeId { get; set; }
    }
}
