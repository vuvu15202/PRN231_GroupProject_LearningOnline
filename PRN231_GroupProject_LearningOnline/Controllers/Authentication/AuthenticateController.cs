using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231_GroupProject_LearningOnline.Entities.DTO;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Services;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PRN231_GroupProject_LearningOnline.Authorization
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IUserService _userService;
        private DonationWebApp_v2Context _context = new DonationWebApp_v2Context();

        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(AuthenticateRequest model)
        {
            var checkUser = _context.Users.SingleOrDefault(u => u.UserName == model.UserName);
            if (checkUser == null)
            {
                try
                {
                    var newUser = new User();
                    newUser.UserName = model.UserName;
                    newUser.Password = BCryptNet.HashPassword(model.Password);
                    newUser.FirstName = "userFirstName";
                    newUser.LastName = "userLastName";
                    newUser.Phone = "";
                    newUser.Address = "";
                    newUser.Email = model.Email;

                    _context.Users.Add(newUser);
                    _context.SaveChanges();

                    var userRole = new UserRole();
                    userRole.RoleId = 3;
                    userRole.UserId = newUser.UserId;
                    _context.UserRoles.Add(userRole);
                    _context.SaveChanges();
                    Console.WriteLine(userRole.RoleId);

                    return Ok(new AuthenticateResponse(newUser));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException);
                }

            }
            else
            {
                return BadRequest("This username have already exist!");
            }
        }

    }
}
