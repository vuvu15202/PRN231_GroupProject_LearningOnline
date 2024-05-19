using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.SearchModels;
using X.PagedList;

namespace PRN231_GroupProject_LearningOnline.Controllers.APIs
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IMapper _mapper;

        public AdminController(DonationWebApp_v2Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize(RoleEnum.Admin)]
        [HttpGet]
        public IActionResult getProjects([FromQuery] SearchProject searchProject)
        {
            var list = _context.FundraisingProjects.Include(x => x.Orders).AsQueryable();
            Console.WriteLine(searchProject.PageSize);
            Console.WriteLine(searchProject.Page);
            if (!string.IsNullOrWhiteSpace(searchProject.Keyword))
            {
                list = list.Where(l => l.Title.ToLower().Contains(searchProject.Keyword.ToLower()));
            }
            var listResult = (PagedList<FundraisingProject>)list.ToPagedList(searchProject.Page, searchProject.PageSize);
            var listView = _mapper.Map<List<ProjectDTO>>(listResult);
            var result = new
            {
                listView,
                listResult.PageCount,
                listResult.PageNumber,
                listResult.IsFirstPage,
                listResult.IsLastPage,
            };
            return Ok(result);
        }

        [Authorize(RoleEnum.Admin)]
        [HttpPost]
        public IActionResult updateProject([FromBody] ChangeStatus changeStatus)
        {
            var project = _context.FundraisingProjects.SingleOrDefault(x => x.ProjectId == changeStatus.Id);
            Console.WriteLine(changeStatus.Id);
            if (project == null)
            {
                return Ok("No project found");
            }
            if (changeStatus.Status == (int) ProjectStatusEnum.Continuing)
            {
                project.Discontinued = true;
            } else
            {
                project.Discontinued = false;
            }
            _context.FundraisingProjects.Update(project);
            _context.SaveChanges();
            var result = "Updated";
            return Ok(result);
        }
    }
}
