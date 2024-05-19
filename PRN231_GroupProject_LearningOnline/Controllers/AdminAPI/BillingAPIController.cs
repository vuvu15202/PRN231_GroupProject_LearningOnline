using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;

namespace PRN231_GroupProject_LearningOnline.Controllers.AdminAPI
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BillingAPIController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;

        public BillingAPIController(DonationWebApp_v2Context context)
        {
            _context = context;
        }


        [Authorize(RoleEnum.Admin)]
        [HttpGet("report")]
        public IActionResult GetReport()
        {
            var totalamount = _context.Orders.Where(o => o.ErrorCode.Equals("0") || o.ErrorCode.Equals("00")).ToList().Sum(o => int.Parse(o.Amount));
            var amountofmonth = _context.Orders.
                Where(o => (o.ErrorCode.Equals("0") || o.ErrorCode.Equals("00"))
                    && (o.DateOfDonation.Value.Year == DateTime.Now.Year)
                    && (o.DateOfDonation.Value.Month == DateTime.Now.Month))
                .ToList().Sum(o => int.Parse(o.Amount));


            return Ok(new Report() { TotalAmount = totalamount, AmountOfMonth = amountofmonth });
        }

        [Authorize(RoleEnum.Admin)]
        [HttpGet("projectbills")]
        public IActionResult GetAllProjectsAndBiling()
        {
            var projects = _context.FundraisingProjects.Include(p => p.Orders).ToList();
            var projectDTOs = projects.Select(d => new FundraisingProjectDTO()
            {
                ProjectId = d.ProjectId,
                Type = d.Type,
                TypeText = Enum.GetName(typeof(TypeEnums), d.Type),
                Title = d.Title,
                Story = d.Story,
                Image = d.Image,
                TargetAmount = d.TargetAmount,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                Discontinued = d.Discontinued,
                Total = _context.Orders.Where(o => o.ProjectId == d.ProjectId).ToList().Sum(o => int.Parse(o.Amount)),
                Orders = d.Orders.Select(o => new OrderDTO()
                {
                    OrderId = o.OrderId,
                    ProjectId = o.ProjectId,
                    PaymentMethod = o.PaymentMethod,
                    BankCode = o.BankCode,
                    Amount = o.Amount,
                    OrderInfo = o.OrderInfo,
                    ErrorCode = o.ErrorCode,
                    LocalMessage = o.LocalMessage,
                    DateOfDonation = o.DateOfDonation!.Value.ToString("dd MMMM yyyy, 'at' hh:mm:ss tt"),
                }).ToList()
                
            });
            return Ok(projectDTOs);
        }

        [Authorize(RoleEnum.Admin)]
        [HttpGet("projectbill/{Id}")]
        public IActionResult GetProjectsAndBilingByProjectId(int Id)
        {
            var d = _context.FundraisingProjects.Include(p => p.Orders).SingleOrDefault(p => p.ProjectId == Id);
            var projectDTO = new FundraisingProjectDTO()
            {
                ProjectId = d.ProjectId,
                Type = d.Type,
                Title = d.Title,
                Story = d.Story,
                Image = d.Image,
                TargetAmount = d.TargetAmount,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                Discontinued = d.Discontinued,
                Total = _context.Orders
                .OrderByDescending(o => o.DateOfDonation)
                .Where(o => o.ProjectId == d.ProjectId).ToList().Sum(o => int.Parse(o.Amount)),
                Orders = d.Orders.Select(o => new OrderDTO()
                {
                    OrderId = o.OrderId,
                    ProjectId = o.ProjectId,
                    PaymentMethod = o.PaymentMethod,
                    BankCode = o.BankCode,
                    Amount = o.Amount,
                    OrderInfo = o.OrderInfo,
                    ErrorCode = o.ErrorCode,
                    LocalMessage = o.LocalMessage,
                    DateOfDonation = o.DateOfDonation!.Value.ToString("dd MMMM yyyy, 'at' hh:mm:ss tt"),
                }).ToList()

            };
            return Ok(projectDTO);
        }

        [Authorize(RoleEnum.Admin)]
        [HttpGet("handlingbills")]
        public IActionResult GetAllHandingBill()
        {
            var orders = _context.Orders
                .Include(o => o.Project)
                .Where(o => !o.ErrorCode.Equals("00") && !o.ErrorCode.Equals("0"))
                .OrderByDescending(o => o.DateOfDonation)
                .ToList();
            var orderDto = orders.Select(d => new OrderDTO()
            {
                OrderId = d.OrderId,
                ProjectId = d.ProjectId,
                PaymentMethod = d.PaymentMethod,
                BankCode = d.BankCode,
                Amount = d.Amount,
                OrderInfo = d.OrderInfo,
                ErrorCode = d.ErrorCode,
                LocalMessage = d.LocalMessage,
                DateOfDonation = d.DateOfDonation!.Value.ToString("dd MMMM yyyy, 'at' hh:mm:ss tt"),
                Project = new FundraisingProjectDTO()
                {
                    ProjectId = d.Project!.ProjectId,
                    Type = d.Project.Type,
                    Title = d.Project.Title,
                    Story = d.Project.Story,
                    Image = d.Project.Image,
                    TargetAmount = d.Project.TargetAmount,
                    StartDate = d.Project.StartDate,
                    EndDate = d.Project.EndDate,
                    Discontinued = d.Project.Discontinued
                }
            });
            return Ok(orderDto);
        }

        [Authorize(RoleEnum.Admin)]
        [HttpPost("GetBillByDate")]
        public IActionResult GetBillByDate([FromBody]filterDateOfDonation filterDate)
        {
            List<Order> orders;
            if (filterDate.fromDate == null && filterDate.toDate == null)
            {
                orders = _context.Orders
                .Include(o => o.Project).OrderByDescending(o => o.DateOfDonation)
                .ToList();
                
            }
            else
            {
                orders = _context.Orders
                .Include(o => o.Project)
                .Where(o => (o.DateOfDonation >= filterDate.fromDate && o.DateOfDonation <= filterDate.toDate)
                                    || (o.DateOfDonation >= filterDate.fromDate && o.DateOfDonation == null)
                                    || (o.DateOfDonation == null && o.DateOfDonation <= filterDate.toDate))
                .OrderByDescending(o => o.DateOfDonation).ToList();
            }
            var orderDto = orders.Select(d => new OrderDTO()
            {
                OrderId = d.OrderId,
                ProjectId = d.ProjectId,
                PaymentMethod = d.PaymentMethod,
                BankCode = d.BankCode,
                Amount = d.Amount,
                OrderInfo = d.OrderInfo,
                ErrorCode = d.ErrorCode,
                LocalMessage = d.LocalMessage,
                DateOfDonation = d.DateOfDonation!.Value.ToString("dd MMMM yyyy, 'at' hh:mm:ss tt"),
                Project = new FundraisingProjectDTO()
                {
                    ProjectId = d.Project!.ProjectId,
                    Type = d.Project.Type,
                    Title = d.Project.Title,
                    Story = d.Project.Story,
                    Image = d.Project.Image,
                    TargetAmount = d.Project.TargetAmount,
                    StartDate = d.Project.StartDate,
                    EndDate = d.Project.EndDate,
                    Discontinued = d.Project.Discontinued
                }
            });
            return Ok(orderDto);
        }
    }
    public class filterDateOfDonation
    {
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }

    }

    public class Report
    {
        public long TotalAmount { get; set; }
        public long AmountOfMonth { get; set; }
    } 
}
