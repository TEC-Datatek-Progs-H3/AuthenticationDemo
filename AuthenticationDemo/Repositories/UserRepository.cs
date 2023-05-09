using Microsoft.EntityFrameworkCore;

namespace AuthenticationDemoAPI.Repositories;
public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User> CreateAsync(User user);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByIdAsync(int userId);
    Task<User> UpdateAsync(int userId, User user);
}

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        if (_context.User.Any(u => u.Email == user.Email))
        {
            throw new Exception("Email " + user.Email + " is not available");
        }

        if (_context.User.Any(u => u.Username == user.Username))
        {
            throw new Exception("Username " + user.Username + " is not available");
        }

        _context.User.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.User.ToListAsync();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        return await _context.User.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User> UpdateAsync(int userId, User user)
    {
        User updateUser = await _context.User.FirstOrDefaultAsync(u => u.Id == userId);

        if (updateUser != null)
        {
            if (_context.User.Any(u => u.Id != userId && u.Email == user.Email))
            {
                throw new Exception("Email " + user.Email + " is not available");
            }

            if (_context.User.Any(u => u.Id != userId && u.Username == user.Username))
            {
                throw new Exception("Username " + user.Username + " is not available");
            }

            updateUser.Email = user.Email;
            updateUser.Username = user.Username;

            if (user.Password != null)
            {
                updateUser.Password = user.Password;
            }
        }

        return updateUser;
    }
}