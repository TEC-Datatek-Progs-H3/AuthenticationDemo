//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationDemoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService, IJwtUtils jwtUtils)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] LoginRequest login)
    {
        try
        {
            LoginResponse user = await _userService.AuthenticateUserAsync(login);
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);

        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUser newUser)
    {
        try
        {
            UserResponse user = await _userService.RegisterUserAsync(newUser);

            return Ok(user);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }


    [Authorize(Role.Admin)]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            List<UserResponse> users = await _userService.GetAllAsync();

            if (users == null)
            {
                return Problem("Got no data, not even an empty list, this is unexpected");
            }

            if (users.Count == 0)
            {
                return NoContent();
            }

            return Ok(users);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    [Authorize(Role.Admin, Role.User)]
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int userId)
    {
        try
        {
            // only admins can access other user records
            UserResponse currentUser = (UserResponse)HttpContext.Items["User"];
            if (currentUser != null && userId != currentUser.Id && currentUser.Role != Role.Admin)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }

            UserResponse user = await _userService.GetByIdAsync(userId);

            if (user == null)
            {
                return NoContent();
            }

            return Ok(user);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }



}