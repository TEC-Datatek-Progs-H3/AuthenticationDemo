namespace AuthenticationDemoAPI.DTOs;

public class HeroResponse
{
    public int Id { get; set; }
    public string HeroName { get; set; } = string.Empty;
    public string RealName { get; set; } = string.Empty;
    public string Place { get; set; } = string.Empty;
    public short DebutYear { get; set; } = 0;
}
