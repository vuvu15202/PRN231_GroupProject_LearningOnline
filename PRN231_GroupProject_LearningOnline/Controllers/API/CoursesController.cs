using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;

        public CoursesController(DonationWebApp_v2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            foreach (var course in courses)
            {

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

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
    }
}
