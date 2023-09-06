using Playmaker.Models;

namespace Playmaker.Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<bool> ExistAsync(int userId);
    Task<bool> ExistAsync(string email);
    Task<User?> GetAsync(int userId);
    Task<User?> GetAsync(string email);
}