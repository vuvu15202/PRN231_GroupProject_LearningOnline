using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;

        public CommentController(DonationWebApp_v2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCommentAsync()
        {
            var response = await _context.Comments.ToListAsync();

            return Ok(_mapper.Map<List<CommentDTO>>(response));
        }


        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCommentByCourseIdAsync(int courseId)
        {
            var response = await _context.Comments.Include(c => c.Replies).Where(c => c.CourseId == courseId).ToListAsync();

            return Ok(_mapper.Map<List<CommentDTO>>(response));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CreateComment request)
        {

            var comment = _mapper.Map<Comment>(request);
            var user = (User)HttpContext.Items["User"];
            comment.UserName = user.UserName;

            try
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<CommentDTO>(comment));
            }catch (Exception ex)
            {
                return Conflict();
            }
        }
        [HttpPost("CreateReply")]
        [Authorize]
        public async Task<IActionResult> CreateReply(CreateReply request)
        {
            var reply = _mapper.Map<Reply>(request);
            var user = (User)HttpContext.Items["User"];
            reply.UserName = user.UserName;
            try
            {
                _context.Replies.Add(reply);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<ReplyDTO>(reply));
            }
            catch (Exception ex)
            {
                return Conflict();
            }

        }

    }
}
