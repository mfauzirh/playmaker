using Playmaker.Dtos;

namespace Playmaker.Services;

public interface IUserService
{
    Task<UserResponse> DeleteAsync(int userId);
    Task<UserResponse> GetAsync(int userId);
    int? GetMyUserId();
    Task<UserResponse> UpdateAsync(int userId, UserUpdateRequest request);
}