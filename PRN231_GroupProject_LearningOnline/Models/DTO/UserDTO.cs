namespace PRN231_GroupProject_LearningOnline.Entities.DTO;

using PRN231_GroupProject_LearningOnline.Models;
using System.Text.Json.Serialization;

public class UserDTO
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public List<RoleEnum> Roles { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }
}