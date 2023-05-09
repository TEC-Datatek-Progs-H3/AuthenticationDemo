using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemoAPI.DTOs;

public class HeroRequest
{
    [Required]
    [StringLength(32, ErrorMessage = "HeroName must not contain more than 32 chars")]
    public string HeroName { get; set; } = string.Empty;

    [Required]
    [StringLength(32, ErrorMessage = "RealName must not contain more than 32 chars")]
    public string RealName { get; set; } = string.Empty;

    [Required]
    [StringLength(32, ErrorMessage = "Place must not contain more than 32 chars")]
    public string Place { get; set; } = string.Empty;

    [Required]
    [Range(1900, 2500, ErrorMessage = "DebutYear must be between 1900 and 2500")]
    public short DebutYear { get; set; } = 1900;

}
