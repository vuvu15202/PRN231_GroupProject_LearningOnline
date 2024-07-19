using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Models.DTO;
using PRN231_GroupProject_LearningOnline.Models;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Models.SearchModels;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PRN231_GroupProject_LearningOnline.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;

        public AccountController(DonationWebApp_v2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUserProfile()
        {
            var u = (User)HttpContext.Items["User"];
            var user = new
            {
                Phonenumber = u.Phone,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address
            };

            return Ok(user);
        }

        [HttpPut]
        public IActionResult UpdateProfile([FromBody] UserProfileDTO model)
        {
            var user = (User)HttpContext.Items["User"];

            try
            {
                var userToUpdate = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (userToUpdate == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                // Cập nhật thông tin từ model vào userToUpdate
                userToUpdate.FirstName = model.FirstName;
                userToUpdate.LastName = model.LastName;
                userToUpdate.Email = model.Email;
                userToUpdate.Phone = model.PhoneNumber;
                userToUpdate.Address = model.Address;

                _context.Update(userToUpdate);  

                _context.SaveChanges();

                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var user = (User)HttpContext.Items["User"];

            try
            {
                var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
                if (userToUpdate == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                if (!VerifyPassword(userToUpdate.Password, model.OldPassword))
                {
                    return BadRequest(new { message = "Old password is incorrect" });
                }

                if (model.NewPassword != model.ConfirmPassword)
                {
                    return BadRequest(new { message = "New password and confirm password do not match" });
                }

                userToUpdate.Password = HashPassword(model.NewPassword);

                _context.Users.Update(userToUpdate);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Password updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        private bool VerifyPassword(string savedPasswordHash, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, savedPasswordHash);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);

        }
    }


    public class UserProfileDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}