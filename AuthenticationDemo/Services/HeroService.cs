namespace AuthenticationDemoAPI.Services;

public interface IHeroService
{
    Task<List<HeroResponse>> GetAllAsync();
    Task<HeroResponse> GetByIdAsync(int superHeroId);
    Task<HeroResponse> CreateAsync(HeroRequest newHero);
    Task<HeroResponse> UpdateAsync(int superHeroId, HeroRequest updateHero);
    Task<HeroResponse> DeleteAsync(int superHeroId);
}

public class HeroService : IHeroService
{
    private readonly IHeroRepository _heroRepository;

    public HeroService(IHeroRepository heroRepository)
    {
        _heroRepository = heroRepository;
    }

    public async Task<List<HeroResponse>> GetAllAsync()
    {
        List<Hero> superHeroes = await _heroRepository.GetAllAsync();

        if (superHeroes != null)
        {
            return superHeroes.Select(superHero => MapHeroToHeroResponse(superHero)).ToList();
        }

        return null;
    }

    public async Task<HeroResponse> GetByIdAsync(int superHeroId)
    {
        Hero superHero = await _heroRepository.GetByIdAsync(superHeroId);

        if (superHero != null)
        {
            return MapHeroToHeroResponse(superHero);
        }

        return null;
    }

    public async Task<HeroResponse> CreateAsync(HeroRequest superHeroRequest)
    {
        Hero superHero = MapHeroRequestToHero(superHeroRequest);

        Hero insertedHero = await _heroRepository.CreateAsync(superHero);

        if (insertedHero != null)
        {
            return MapHeroToHeroResponse(insertedHero);
        }

        return null;
    }

    public async Task<HeroResponse> UpdateAsync(int superHeroId, HeroRequest superHeroRequest)
    {
        Hero superHero = MapHeroRequestToHero(superHeroRequest);

        Hero updatedHero = await _heroRepository.UpdateAsync(superHeroId, superHero);

        if (updatedHero != null)
        {
            return MapHeroToHeroResponse(updatedHero);
        }

        return null;
    }

    public async Task<HeroResponse> DeleteAsync(int superHeroId)
    {
        Hero deletedHero = await _heroRepository.DeleteAsync(superHeroId);

        if (deletedHero != null)
        {
            return MapHeroToHeroResponse(deletedHero);
        }

        return null;
    }

    private static Hero MapHeroRequestToHero(HeroRequest heroRequest) => new Hero
    {
        HeroName = heroRequest.HeroName,
        RealName = heroRequest.RealName,
        Place = heroRequest.Place,
        DebutYear = heroRequest.DebutYear
    };


    private static HeroResponse MapHeroToHeroResponse(Hero hero) => new HeroResponse
    {
        Id = hero.Id,
        HeroName = hero.HeroName,
        RealName = hero.RealName,
        DebutYear = hero.DebutYear,
        Place = hero.Place
    };
}