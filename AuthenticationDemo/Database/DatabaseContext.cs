using Microsoft.EntityFrameworkCore;

namespace AuthenticationDemoAPI.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    public DbSet<User> User { get; set; }
    public DbSet<Hero> Hero { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "Albert",
                Email = "albert@mail.dk",
                Password = "Test1234",
                Role = Role.Admin
            },
            new User
            {
                Id = 2,
                Username = "Benny",
                Email = "benny@mail.dk",
                Password = "Test1234",
                Role = Role.User
            });

        modelBuilder.Entity<Hero>().HasData(
            new Hero
            {
                Id = 1,
                HeroName = "Superman",
                RealName = "Clark Kent",
                Place = "Metropolis",
                DebutYear = 1938
            },
            new Hero
            {
                Id = 2,
                HeroName = "Iron Man",
                RealName = "Tony Stark",
                Place = "Malibu",
                DebutYear = 1963
            });
    }
}