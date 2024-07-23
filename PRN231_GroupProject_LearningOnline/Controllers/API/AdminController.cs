using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.SearchModels;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Authorization;
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
            if (changeStatus.Status == (int)ProjectStatusEnum.Continuing)
            {
                project.Discontinued = true;
            }
            else
            {
                project.Discontinued = false;
            }
            _context.FundraisingProjects.Update(project);
            _context.SaveChanges();
            var result = "Updated";
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();

            var userDtos = users.Result.Select(user => new UserListDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Active = user.Active,
                Role = user.UserRoles.Where(ur => ur.UserId == user.UserId).Select(
                    ur => new RoleDTO
                    {
                        RoleId = ur.RoleId,
                        RoleName = ur.Role.RoleName
                    }
                ).FirstOrDefault(),
            }).ToList();

            return Ok(userDtos);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserEditDTO userEditDTO)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userEditDTO.UserId);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = userEditDTO.FirstName;
            user.LastName = userEditDTO.LastName;
            user.Email = userEditDTO.Email;
            user.Phone = userEditDTO.Phone;
            user.Address = userEditDTO.Address;
            user.Active = userEditDTO.Active;

            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == userEditDTO.UserId);
            if (user == null)
            {
                return NotFound();
            }

            userRole.RoleId = userEditDTO.RoleId;

            var result = _context.Users.Update(user);
            var result2 = _context.UserRoles.Update(userRole);
            _context.SaveChanges();
            return Ok(userEditDTO);
        }

        [HttpGet]
        public IActionResult GetAllRole()
        {
            var roles = _context.Roles.ToListAsync();
            var roleDTOs = roles.Result.Select(
                    r => new RoleDTO
                    {
                        RoleId = r.RoleId,
                        RoleName = r.RoleName,
                    }
                ).ToList();
            return Ok(roleDTOs);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            var user = new User();

            user.UserName = createUserDTO.UserName;
            user.FirstName = createUserDTO.FirstName;
            user.LastName = createUserDTO.LastName;
            user.Password = BCrypt.Net.BCrypt.HashPassword(createUserDTO.Password);
            user.Email = createUserDTO.Email;
            user.Phone = createUserDTO.Phone;
            user.Address = createUserDTO.Address;
            user.Active = createUserDTO.Active;

            _context.Users.Add(user);
            _context.SaveChanges();

            var userRole = new UserRole();
            userRole.UserId = user.UserId;
            userRole.RoleId = createUserDTO.RoleId;
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();

            return Ok();
        }

        public class UserListDTO
        {
            public int UserId { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public bool Active { get; set; }
            public RoleDTO Role { get; set; }
        }

        public class UserEditDTO
        {
            public int UserId { get; set; }
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public bool Active { get; set; }
            public int RoleId { get; set; }
        }

        public class RoleDTO
        {
            public int RoleId { get; set; }
            public string? RoleName { get; set; }
        }

        public class CreateUserDTO
        {
            public string UserName { get; set; } = null!;
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string Password { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public bool Active { get; set; }
            public int RoleId { get; set; }
        }
    }
}