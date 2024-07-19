using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.temp;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;

        public LessonsController(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        // GET: api/Lessons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
          if (_context.Lessons == null)
          {
              return NotFound();
          }
            return await _context.Lessons.ToListAsync();
        }

        // GET: api/Lessons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
          if (_context.Lessons == null)
          {
              return NotFound();
          }
            var lesson = await _context.Lessons.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return lesson;
        }

        // PUT: api/Lessons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesson(int id, Lesson lesson)
        {
            if (id != lesson.LessonId)
            {
                return BadRequest();
            }

            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(id))
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

        // POST: api/Lessons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Lesson>> PostLesson(Lesson lesson)
        //{
        //  if (_context.Lessons == null)
        //  {
        //      return Problem("Entity set 'DonationWebApp_v2Context.Lessons'  is null.");
        //  }
        //    _context.Lessons.Add(lesson);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetLesson", new { id = lesson.LessonId }, lesson);
        //}

        // DELETE: api/Lessons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            if (_context.Lessons == null)
            {
                return NotFound();
            }
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LessonExists(int id)
        {
            return (_context.Lessons?.Any(e => e.LessonId == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Postt([FromForm] LessonModel lesson)
        {
            JObject fileContentJson = null;
            var fileContent = "";
            if (lesson.Quiz != null && lesson.Quiz.ContentType == "application/json")
            {
                using (var stream = new MemoryStream())
                {
                    await lesson.Quiz.CopyToAsync(stream);
                    stream.Position = 0;

                    using (var reader = new StreamReader(stream))
                    {
                         fileContent = await reader.ReadToEndAsync();
                        //fileContentJson = JObject.Parse(fileContent);

                    }
                }
            }
            var les = new Lesson()
            {
                LessonNum = lesson.LessonNum,
                CourseId = lesson.CourseId,
                Name = lesson.Name,
                Description = lesson.Description,
                VideoUrl = lesson.VideoUrl,
                Quiz = fileContent,
                PreviousLessioNum = lesson.PreviousLessioNum,
            };
            _context.Lessons.Add(les);
            await _context.SaveChangesAsync();

            // Xử lý nội dung JSON từ fileContentJson ở đây
            // Ví dụ: bạn có thể in nội dung của JSON ra console để kiểm tra
            System.Diagnostics.Debug.WriteLine(fileContentJson);

            return Ok(les);
        }
    }

    public class LessonModel
    {
        public int? LessonId { get; set; }
        public int LessonNum { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public IFormFile Quiz { get; set; }
        public int PreviousLessioNum { get; set; }
    }

}
