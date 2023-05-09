using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationDemoAPI.Database.Entites;

public class Hero
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(32)")]
    public string HeroName { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(32)")]
    public string RealName { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(32)")]
    public string Place { get; set; } = string.Empty;

    [Column(TypeName = "smallint")]
    public short DebutYear { get; set; } = 0;
}
