namespace PRN231_GroupProject_LearningOnline.Entities.DTO;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
    public string? Email { get; set; }
}