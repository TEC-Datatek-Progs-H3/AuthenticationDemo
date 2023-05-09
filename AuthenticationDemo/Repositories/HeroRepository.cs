using Microsoft.EntityFrameworkCore;

namespace AuthenticationDemoAPI.Repositories;
public interface IHeroRepository
{
    Task<List<Hero>> GetAllAsync();
    Task<Hero> GetByIdAsync(int heroId);
    Task<Hero> CreateAsync(Hero newHero);
    Task<Hero> UpdateAsync(int heroId, Hero updateHero);
    Task<Hero> DeleteAsync(int heroId);
}
public class HeroRepository : IHeroRepository
{
    private readonly DatabaseContext _context;

    public HeroRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Hero>> GetAllAsync()
    {
        return await _context.Hero.ToListAsync();
    }

    public async Task<Hero> GetByIdAsync(int heroId)
    {
        return await _context.Hero.FirstOrDefaultAsync(s => s.Id == heroId);
    }

    public async Task<Hero> CreateAsync(Hero newHero)
    {
        _context.Hero.Add(newHero);
        await _context.SaveChangesAsync();
        return newHero;
    }

    public async Task<Hero> UpdateAsync(int heroId, Hero updateHero)
    {
        Hero hero = await GetByIdAsync(heroId);
        if (hero != null)
        {
            hero.HeroName = updateHero.HeroName;
            hero.RealName = updateHero.RealName;
            hero.Place = updateHero.Place;
            hero.DebutYear = updateHero.DebutYear;

            await _context.SaveChangesAsync();
        }
        return hero;
    }

    public async Task<Hero> DeleteAsync(int heroId)
    {
        Hero hero = await GetByIdAsync(heroId);
        if (hero != null)
        {
            _context.Hero.Remove(hero);
            await _context.SaveChangesAsync();
        }
        return hero;
    }
}
