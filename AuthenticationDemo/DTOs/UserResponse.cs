namespace AuthenticationDemoAPI.DTOs;

public class UserResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public Role Role { get; set; }
}
