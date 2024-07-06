using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.SearchModels;
using PRN231_GroupProject_LearningOnline.Models;
using X.PagedList;
using PRN231_GroupProject_LearningOnline.Authorization;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;

        public DashboardController(DonationWebApp_v2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(RoleEnum.Admin)]
        [HttpGet("report")]
        public IActionResult getreportdashboard()
        {
            var todayMoney = _context.StudentFees.
                Where(o => (o.ErrorCode.Equals("0") || o.ErrorCode.Equals("00"))
                    && o.DateOfPaid.Value.Year == DateTime.Now.Year
                    && o.DateOfPaid.Value.Month == DateTime.Now.Month)
                .ToList().Sum(o => int.Parse(o.Amount));
            var todayUser = 0;
            var newClient = 10;
            var newEnroll = _context.CourseEnrolls.
                Where(o => o.EnrollDate.Year == DateTime.Now.Year
                    && o.EnrollDate.Month == DateTime.Now.Month).ToList().Count();

            var statistic = getStatistic(null, 2024);
            var courses = _context.Courses.ToList().Select(c => new
            {
                id = c.CourseId,
                name = c.Name
            });



            var studentfees = _context.StudentFees
                .Where(o => !o.ErrorCode.Equals("00") && !o.ErrorCode.Equals("0"))
                .OrderByDescending(o => o.DateOfPaid)
                .ToList();
            var studentfeeDTOs = _mapper.Map<List<StudentFeeDTO>>(studentfees);
            foreach (var studentfeeDTO in studentfeeDTOs)
            {
                var ce = _context.CourseEnrolls.SingleOrDefault(c => c.StudentFeeId == studentfeeDTO.StudentFeeId);
                if (ce != null)
                {
                    var course = _context.Courses.SingleOrDefault(c => c.CourseId == ce.CourseId);
                    studentfeeDTO.Course = _mapper.Map<CourseDTO>(course);
                }

            }


            return Ok(
                new { todayMoney= todayMoney, 
                        todayUser= todayUser,
                        newClient= newClient,
                        newEnroll= newEnroll,
                        statistic = statistic,
                        courses = courses,
                        handlingstudentfees= studentfeeDTOs
                });
        }

        [Authorize(RoleEnum.Admin)]
        [HttpGet("getStatistic")]
        public IActionResult getStatisticDashboard(int? courseId, int year = 2024)
        {
            return Ok(new { statistic = getStatistic(courseId, year) });
        }


        List<int> getStatistic(int? courseId, int year = 2024)
        {
            var list = new List<int>();
            if(courseId.HasValue)
            {
                for (int i= 1; i <= 12; i++)
                {
                    int value = _context.StudentFees.Include(c => c.CourseEnroll).
                        Where(o => (o.ErrorCode.Equals("0") || o.ErrorCode.Equals("00"))
                            && o.DateOfPaid.Value.Year == year
                            && o.DateOfPaid.Value.Month ==i
                            && o.CourseEnroll!.CourseId == courseId)
                        .ToList().Sum(o => int.Parse(o.Amount));
                    list.Add(value);
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    int value = _context.StudentFees.
                        Where(o => (o.ErrorCode.Equals("0") || o.ErrorCode.Equals("00"))
                            && o.DateOfPaid.Value.Year == year
                            && o.DateOfPaid.Value.Month == i)
                        .ToList().Sum(o => int.Parse(o.Amount));
                    list.Add(value);
                }
            }
            return list;
        }
    }
}
