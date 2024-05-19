namespace PRN231_GroupProject_LearningOnline.Entities.DTO;

using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Entities;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public string RedirectUrl { get; set; }
    public List<Role> Role { get; set; }
    public string JwtToken { get; set; }

    public AuthenticateResponse(User user, List<Role> role, string token)
    {
        Id = user.UserId;
        FirstName = user.FirstName;
        LastName = user.LastName;
        UserName = user.UserName;
        Role = role;
        JwtToken = token;
        if (role.FirstOrDefault()!.RoleName.Equals("ADMIN")) RedirectUrl = "/admin/dashboard";
        else RedirectUrl = "/home/index";
    }

    public AuthenticateResponse(User user)
    {
        Id = user.UserId;
        FirstName = user.FirstName;
        LastName = user.LastName;
        UserName = user.UserName;
        RedirectUrl = "/auth/login";
    }
}