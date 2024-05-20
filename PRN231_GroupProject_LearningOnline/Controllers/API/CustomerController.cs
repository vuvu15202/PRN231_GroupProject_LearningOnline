using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        public CustomerController(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        [HttpGet("projects/{quantity}")]
        public IActionResult GetProjectsFromLast(int quantity)
        {
            var projects = _context.FundraisingProjects.OrderByDescending(p => p.ProjectId).Take(quantity).ToList();
            return Ok(projects);
        }

        [HttpGet("project/{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _context.FundraisingProjects.FirstOrDefault(p => p.ProjectId == id);
            return Ok(new { project });
        }

        [HttpGet("projects/type")]
        public IActionResult getProjectByType()
        {
            var typeEnumType = typeof(TypeEnums);
            var projects = _context.FundraisingProjects.Select(p => new { type = p.Type, text = Enum.GetName(typeEnumType, p.Type) }).Distinct().ToList();

            return Ok(projects);
        }

        [HttpGet("projects/pagination/{pageNo}/{type}")]
        public IActionResult GetProjectPagination(int pageNo, int type)
        {
            int TotalProjects = _context.FundraisingProjects.Where(p => p.Type == type).Count();
            int PageSize = 4, currentPage = 0;
            int TotalPage = (int)Math.Ceiling(TotalProjects / (double)PageSize);
            if (pageNo != 0)
                currentPage = pageNo;
            else if (pageNo > TotalPage)
                currentPage = TotalPage;
            var projects = _context.FundraisingProjects.Where(p => p.Type == type).Skip((currentPage - 1) * PageSize).Take(PageSize).ToList();

            return Ok(new { totalPage = TotalPage, pageNo, projects });
        }

        [HttpGet("projects/pagination/{pageNo}")]
        public IActionResult GetProjectPaginationAll(int pageNo)
        {
            int TotalProjects = _context.FundraisingProjects.Count();
            int PageSize = 4, currentPage = 0;
            int TotalPage = (int)Math.Ceiling(TotalProjects / (double)PageSize);
            if (pageNo != 0)
                currentPage = pageNo;
            else if (pageNo > TotalPage)
                currentPage = TotalPage;
            var projects = _context.FundraisingProjects.Skip((currentPage - 1) * PageSize).Take(PageSize).ToList();

            return Ok(new { totalPage = TotalPage, pageNo, projects });
        }

        [HttpGet("project/{projectId}/donatesTop/{quantity}")]
        public IActionResult GetDonateTop(int projectId, int quantity)
        {
            var donates = _context.Orders.Where(o => o.ProjectId == projectId).OrderBy(o => o.Amount).Take(quantity).ToList();
            return Ok(donates);
        }

        [HttpGet("projects/{id}/amount")]
        public IActionResult GetAmount(int id)
        {
            var amount = _context.Orders.Where(p => p.ProjectId == id).ToList();
            double sum = 0;
            int totalDonate = 0;
            foreach (var item in amount)
            {
                sum += double.Parse(item.Amount);
                totalDonate++;
            }
            return Ok(new { TotalAmount = sum, TotalDonate = totalDonate });
        }

        [HttpGet("news/{quantity}")]
        public IActionResult GetNewsFromLast(int quantity)
        {
            return Ok();
        }
    }
}
