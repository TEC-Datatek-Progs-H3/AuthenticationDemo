using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemoAPI.DTOs;

public class UpdateUser
{
    [Required]
    [StringLength(128, ErrorMessage = "Email must be less than 128 chars")]
    public string Email { get; set; }

    [Required]
    [StringLength(32, ErrorMessage = "Username must be less than 32 chars")]
    public string Username { get; set; }

    [StringLength(32, ErrorMessage = "Username must be less than 32 chars")]
    public string Password { get; set; }

    [Required]
    public Role Role { get; set; }
}