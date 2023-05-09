using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationDemoAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HeroController : ControllerBase
{
    private readonly IHeroService _heroService;

    public HeroController(IHeroService heroService)
    {
        _heroService = heroService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            List<HeroResponse> heroes = await _heroService.GetAllAsync();

            if (heroes.Count == 0)
            {
                return NoContent();
            }

            return Ok(heroes);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [Authorize(Role.Admin)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] HeroRequest newHero)
    {
        try
        {
            HeroResponse heroResponse = await _heroService.CreateAsync(newHero);

            return Ok(heroResponse);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("{heroId}")]
    public async Task<IActionResult> FindByIdAsync([FromRoute] int heroId)
    {
        try
        {
            HeroResponse heroResponse = await _heroService.GetByIdAsync(heroId);
            if (heroResponse is null)
            {
                return NotFound();
            }
            return Ok(heroResponse);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [Authorize(Role.Admin)]
    [HttpPut]
    [Route("{heroId}")]
    public async Task<IActionResult> UpdateByIdAsync([FromRoute] int heroId, [FromBody] HeroRequest updateHero)
    {
        try
        {
            HeroResponse? heroResponse = await _heroService.UpdateAsync(heroId, updateHero);
            if (heroResponse is null)
            {
                return NotFound();
            }

            return Ok(heroResponse);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [Authorize(Role.Admin)]
    [HttpDelete]
    [Route("{heroId}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int heroId)
    {
        try
        {
            HeroResponse? heroResponse = await _heroService.DeleteAsync(heroId);
            if (heroResponse is null)
            {
                return NotFound();
            }
            return Ok(heroResponse);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
