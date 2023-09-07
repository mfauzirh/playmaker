using Playmaker.Dtos;

namespace Playmaker.Services;

public interface IUserService
{
    Task<UserResponse> GetAsync(int userId);
    int? GetMyUserId();
}