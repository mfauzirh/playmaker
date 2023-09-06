using Microsoft.EntityFrameworkCore;
using Playmaker.Data;
using Playmaker.Models;

namespace Playmaker.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> ExistAsync(int userId)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId);
    }

    public async Task<User?> GetAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }
}