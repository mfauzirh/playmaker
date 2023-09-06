using Playmaker.Models;

namespace Playmaker.Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<bool> ExistAsync(int userId);
    Task<User?> GetAsync(int userId);
}