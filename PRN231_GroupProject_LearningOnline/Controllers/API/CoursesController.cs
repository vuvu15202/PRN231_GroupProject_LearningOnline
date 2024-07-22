using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.temp;
using static System.Net.Mime.MediaTypeNames;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public CoursesController(DonationWebApp_v2Context context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
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

            return Ok(_mapper.Map<CourseDTO>(course));
        }

        //// PUT: api/Courses/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCourse(int id, Course course)
        //{
        //    if (id != course.CourseId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(course).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CourseExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Courses
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Course>> PostCourse(Course course)
        //{
        //  if (_context.Courses == null)
        //  {
        //      return Problem("Entity set 'DonationWebApp_v2Context.Courses'  is null.");
        //  }
        //    _context.Courses.Add(course);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        //}

        [Authorize(RoleEnum.Student)]
        [HttpPost("grade")]
        public async Task<ActionResult> Grade([FromBody] List<string> answer) //question-2-5-1-B =      question-courseId-lessonId-questionNo-B
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

            var user = (User)HttpContext.Items["User"];
            var coursenroll = _context.CourseEnrolls
                .Where(c => c.CourseId == Int16.Parse(answerInfo[1]) && c.UserId == user.UserId).SingleOrDefault();

            if (String.IsNullOrEmpty(coursenroll!.Grade))
            {
                coursenroll.Grade = $"{result}";

            }
            else
            {
                coursenroll.Grade = coursenroll.Grade + $";{result}";
            }
            //update grade
            _context.CourseEnrolls.Update(coursenroll);

            //update course status
            var checkLesson = _context.Lessons.Where(l => l.CourseId == Int16.Parse(answerInfo[1])).OrderBy(l => l.LessonNum).LastOrDefault();
            if (lessonInfo.LessonId == checkLesson.LessonId)
            {
                coursenroll.CourseStatus = 1;
            }
            _context.SaveChanges();

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
            course.IsDelete = true;
            _context.Courses.Update(course);
            //_context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }



        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Course>>> SearchCourses(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name parameter is required.");
            }

            var courses = await _context.Courses
                                        .Where(c => c.Name.Contains(name))
                                        .ToListAsync();

            if (!courses.Any())
            {
                return NotFound("No courses found.");
            }

            return Ok(courses);
        }

        [HttpGet("filterByCategory")]
        public async Task<ActionResult<IEnumerable<Course>>> FilterCoursesByCategory(int categoryId)
        {
            var courses = await _context.Courses
                                        .Include(c => c.Category)
                                        .Where(c => c.CategoryId == categoryId)
                                        .ToListAsync();

            if (!courses.Any())
            {
                return NotFound("No courses found in this category.");
            }

            return Ok(courses);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Course>>> FilterCourses(int? categoryId, string name)
        {
            IQueryable<Course> query = _context.Courses.Include(c => c.Category);

            if (categoryId != null && categoryId > 0)
            {
                query = query.Where(c => c.CategoryId == categoryId);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            var courses = await query.ToListAsync();

            if (courses == null || courses.Count == 0)
            {
                return NotFound("No courses found with the specified filters.");
            }

            return Ok(courses);
        }


        [HttpPost]
        public async Task<IActionResult> PostCourse([FromForm] CourseModel course)
        {
            if (course.Image  == null || course.Image.Length == 0)
                return BadRequest("No file uploaded");

            var uploads = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var filePath = Path.Combine(uploads, course.Image.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await course.Image.CopyToAsync(stream);
            }



            var cour = new Course()
            {
                CategoryId = course.CategoryId,
                Name = course.Name,
                Image = "/uploads/" + course.Image.FileName,
                Description = course.Description,
                IsPrivate = course.IsPrivate,
                Price = course.Price,
            };
            _context.Courses.Add(cour);
            await _context.SaveChangesAsync();

            // Xử lý nội dung JSON từ fileContentJson ở đây
            // Ví dụ: bạn có thể in nội dung của JSON ra console để kiểm tra
            System.Diagnostics.Debug.WriteLine("test upload");

            return Ok(cour);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, [FromForm] CourseModel course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }

            var checkCourse = _context.Courses.IgnoreQueryFilters().FirstOrDefault(c => c.CourseId == id);
            if (checkCourse == null)
            {
                return NotFound("Không tìm thấy khóa học!");
            }

            string fileName = "";
            if (course.Image != null )
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var filePath = Path.Combine(uploads, course.Image.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await course.Image.CopyToAsync(stream);
                    fileName = course.Image.FileName;
                }
            }

            try
            {
                checkCourse.CategoryId = course.CategoryId;
                checkCourse.Name = course.Name;
                checkCourse.Image = String.IsNullOrEmpty(fileName) ? checkCourse.Image : "/uploads/" + fileName;
                checkCourse.Description = course.Description;
                checkCourse.IsPrivate = course.IsPrivate;
                checkCourse.Price = course.Price;
                checkCourse.IsDelete = course.IsDelete;

                _context.Update(checkCourse);
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

            return Ok(checkCourse);
        }
    }

    public class CourseModel
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public IFormFile? Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsPrivate { get; set; }
        public long? Price { get; set; } = null!;
        public bool IsDelete { get; set; }

    }
}
