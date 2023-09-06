using Playmaker.Dtos;

namespace Playmaker.Services;

public interface IAuthServices
{
    Task<UserResponse> Register(RegisterRequest request);
    Task<string> Login(LoginRequest request);
}