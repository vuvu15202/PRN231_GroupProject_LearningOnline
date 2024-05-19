namespace PRN231_GroupProject_LearningOnline.Services;

using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BCryptNet = BCrypt.Net.BCrypt;
using PRN231_GroupProject_LearningOnline.Models.Entity;
using PRN231_GroupProject_LearningOnline.Entities.DTO;
using PRN231_GroupProject_LearningOnline.Authorization;
using PRN231_GroupProject_LearningOnline.Helpers;
using PRN231_GroupProject_LearningOnline.Models;

public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User GetById(int id);

    UserDTO toDTO(User user, List<Role> roles);
}

public class UserService : IUserService
{
    private DonationWebApp_v2Context _context;
    private IJwtUtils _jwtUtils;
    private readonly AppSettings _appSettings;

    public UserService(
        //PRN231_uniContext context,
        IJwtUtils jwtUtils,
        IOptions<AppSettings> appSettings)
    {
        _context = new DonationWebApp_v2Context();
        _jwtUtils = jwtUtils;
        _appSettings = appSettings.Value;
    }


    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _context.Users.SingleOrDefault(x => x.UserName == model.UserName);

        // validate
        if (user == null || !BCrypt.Verify(model.Password, user.Password))
            throw new AppException("Username or password is incorrect");

        // authentication successful so generate jwt token
        var jwtToken = _jwtUtils.GenerateJwtToken(user);
        var userRoles = _context.Roles.Where(r => r.UserRoles.Any(ur => ur.UserId == user.UserId))
    .ToList();
        return new AuthenticateResponse(user, userRoles, jwtToken);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User GetById(int id) 
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public UserDTO toDTO(User user, List<Role> roles)
    {
        var userDTO = new UserDTO();
        userDTO.UserId = user.UserId;
        userDTO.UserName = user.UserName;
        userDTO.FirstName = user.FirstName;
        userDTO.LastName = user.LastName;

        List<RoleEnum> rolesEnum = new List<RoleEnum>();
        foreach (Role role in roles)
        {
            switch (role.RoleId)
            {
                case 1: rolesEnum.Add(RoleEnum.Admin); break;
                case 2: rolesEnum.Add(RoleEnum.Staff); break;
                default: rolesEnum.Add(RoleEnum.Orginization); break;

            }
        }
        userDTO.Roles = rolesEnum;
        return userDTO;
    }
}