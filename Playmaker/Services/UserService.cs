using System.Net;
using System.Security.Claims;
using AutoMapper;
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
}