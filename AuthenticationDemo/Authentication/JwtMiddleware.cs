

namespace AuthenticationDemoAPI.Authentication;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtUtils jwtUtils)
    {
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        int? userId = jwtUtils.ValidateJwtToken(token);
        if (userId is not null)
        {
            // attach user to context on successful jwt validation
            var user = await userRepository.GetByIdAsync(userId.Value);
            context.Items["User"] = UserService.MapUserTouserResponse(user);
        }

        await _next(context);
    }
}