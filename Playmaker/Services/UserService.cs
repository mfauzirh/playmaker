using System.Net;
using System.Security.Claims;
using AutoMapper;
using Bcrypt = BCrypt.Net.BCrypt;
using Playmaker.Dtos;
using Playmaker.Exceptions;
using Playmaker.Models;
using Playmaker.Repositories;

namespace Playmaker.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<UserResponse> DeleteAsync(int userId)
    {
        User? user = await _userRepository.GetAsync(userId);

        if (user is null)
        {
            throw new ResponseException(HttpStatusCode.NotFound, $"User with id '{userId}' is not found.");
        }

        User deletedUser = await _userRepository.DeleteAsync(user);

        return _mapper.Map<UserResponse>(deletedUser);
    }

    public async Task<UserResponse> GetAsync(int userId)
    {
        User? user = await _userRepository.GetAsync(userId);

        if (user is null)
        {
            throw new ResponseException(HttpStatusCode.NotFound, $"User with id '{userId}' is not found.");
        }

        return _mapper.Map<UserResponse>(user);
    }

    public int? GetMyUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null) return null;

        return int.Parse(userId);
    }

    public async Task<UserResponse> UpdateAsync(int userId, UserUpdateRequest request)
    {
        User? user = await _userRepository.GetAsync(userId);

        if (user is null)
        {
            throw new ResponseException(HttpStatusCode.NotFound, $"User with id '{userId}' is not found.");
        }

        user.Name = request.Name is not null ? request.Name : user.Name;
        user.Email = request.Email is not null ? request.Email : user.Email;
        user.Password = request.Password is not null ? Bcrypt.HashPassword(request.Password) : user.Password;

        User updatedUser = await _userRepository.UpdateAsync(user);

        return _mapper.Map<UserResponse>(updatedUser);
    }
}