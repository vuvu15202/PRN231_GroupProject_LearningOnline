using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.Resourse;
using PRN231_GroupProject_LearningOnline.Models.SearchModels;
using PRN231_GroupProject_LearningOnline.Services;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;
        private readonly ICourseService _services;
        private readonly IUriService _uriService;

        public CoursesController(DonationWebApp_v2Context context, IMapper mapper, ICourseService services, IUriService uriService)
        {
            _context = context;
            _mapper = mapper;
            _services = services;
            _uriService = uriService;
        }




        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            var courses = await _context.Courses.Include(c => c.Lessons).ToListAsync();
            foreach(var course in courses)
            {
                course.Image = string.Concat(_uriService.GetBaseUri(), course.Image);
            }

            return Ok(_mapper.Map<List<CourseDTO>>(courses));
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourse(int id)
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            var course = await _context.Courses.Include(c => c.Lessons).SingleOrDefaultAsync(c => c.CourseId== id);

            if (course == null)
            {
                return NotFound();
            }

			course.Image = string.Concat(_uriService.GetBaseUri(), course.Image);


			return Ok(_mapper.Map<CourseDTO>(course));
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
          if (_context.Courses == null)
          {
              return Problem("Entity set 'DonationWebApp_v2Context.Courses'  is null.");
          }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        [HttpPost("grade")]
        public async Task<ActionResult> Grade([FromBody] List<string> answer)
        {
            if (answer == null || answer.Count ==0)
            {
                return Problem("không tìm thấy câu trả lời!");
            }
            var answerInfo = answer.First().Split('-');
            var lessonInfo = await _context.Lessons.FirstOrDefaultAsync(l => l.LessonId == Int16.Parse(answerInfo[2])) ;
            var quizes = JsonSerializer.Deserialize<List<QuizToGradeDTO>>(_mapper.Map<LessonDTO>(lessonInfo).Quiz);
            int result = 0;
            foreach (var l in answer)
            {
                var temp = l.Split('-');
                int idex = quizes.FindIndex(q => q.questionNo == Int16.Parse(temp[3]));
                if (quizes[idex].correctAnswer.Equals(temp[4]))
                {
                    result += 1;
                }
                
            }


            return Ok(new {result = $"{result}/10"}) ;
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// lấy 3 course có số người enroll nhiều nhất
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTop3")]
        public async Task<IActionResult> GetTop3Course()
        {
            var response = await _services.GetTop3CourseAsync();

            return Ok(response);
        }


        /// <summary>
        /// Lấy course filter theo tên và category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SearchCourse")]
        public async Task<IActionResult> SearchCourseAsync(SearchCourseModel request)
        {
            var response = await _services.GetListCourseAsync(request);
            return Ok(response);
        }


        /// <summary>
        /// Lấy các course đã enroll
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("GetMyCourse")]
        public async Task<IActionResult> GetMyCourseAsync([FromBody] QueryResource query)
        {
            var user = (User)HttpContext.Items["User"];
            var response = await _services.GetListEnrolledCourseAsync(query, user.UserId);
            return Ok(response);
        }



        /// <summary>
        /// api: Tạo Course
        /// </summary>
        /// <param name="resquest"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCourseResquest resquest)
        {
            var user = (User)HttpContext.Items["User"];
            var response = await _services.CreateCourseAsync(resquest,user.UserId);
            if(response.IsSuccess) return Ok(response.Data);
            else return BadRequest(response.Data);

        }


        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
