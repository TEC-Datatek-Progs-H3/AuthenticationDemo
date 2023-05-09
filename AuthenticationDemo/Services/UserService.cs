namespace AuthenticationDemoAPI.Services;

public interface IUserService
{
    Task<List<UserResponse>> GetAllAsync();
    Task<UserResponse> GetByIdAsync(int userId);
    Task<LoginResponse> AuthenticateUserAsync(LoginRequest login);
    Task<UserResponse> RegisterUserAsync(RegisterUser newUser);
    Task<UserResponse> UpdateAsync(int userId, UpdateUser updateUser);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtUtils _jwtUtils;

    public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
    {
        _userRepository = userRepository;
        _jwtUtils = jwtUtils;
    }

    public async Task<LoginResponse> AuthenticateUserAsync(LoginRequest login)
    {
        User user = await _userRepository.GetByEmailAsync(login.Email);
        if (user == null)
        {
            return null;
        }

        if (user.Password == login.Password)
        {
            LoginResponse response = new()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
                Token = _jwtUtils.GenerateJwtToken(user)
            };
            return response;
        }

        return null;
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        List<User> users = await _userRepository.GetAllAsync();

        return users?.Select(user => MapUserTouserResponse(user)).ToList();
    }

    public async Task<UserResponse> GetByIdAsync(int userId)
    {
        User user = await _userRepository.GetByIdAsync(userId);
        return MapUserTouserResponse(user);
    }

    public async Task<UserResponse> RegisterUserAsync(RegisterUser newUser)
    {
        User user = new()
        {
            Email = newUser.Email,
            Username = newUser.Username,
            Password = newUser.Password,
            Role = Helpers.Role.User // force all users created through Register, to Role.User
        };

        user = await _userRepository.CreateAsync(user);

        return MapUserTouserResponse(user);
    }

    public async Task<UserResponse> UpdateAsync(int userId, UpdateUser updateUser)
    {
        User user = new()
        {
            Email = updateUser.Email,
            Username = updateUser.Username,
            Password = updateUser.Password,
            Role = updateUser.Role
        };

        user = await _userRepository.UpdateAsync(userId, user);

        return MapUserTouserResponse(user);
    }

    static public UserResponse MapUserTouserResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            Role = user.Role
        };
    }
}
