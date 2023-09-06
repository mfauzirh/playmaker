using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using Bcrypt = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using Playmaker.Dtos;
using Playmaker.Exceptions;
using Playmaker.Models;
using Playmaker.Repositories;

namespace Playmaker.Services;

public class AuthService : IAuthServices
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<string> Login(LoginRequest request)
    {
        User? user = await _userRepository.GetAsync(request.Email);

        if (user is null)
        {
            throw new ResponseException(HttpStatusCode.NotFound, $"User with email '{request.Email}' is not found.");
        }

        if (!Bcrypt.Verify(request.Password, user.Password))
        {
            throw new ResponseException(HttpStatusCode.Unauthorized, "Email or password wrong.");
        }

        string token = CreateToken(user);

        return token;
    }

    public async Task<UserResponse> Register(RegisterRequest request)
    {
        if (await _userRepository.ExistAsync(request.Email))
        {
            throw new ResponseException(HttpStatusCode.Conflict, $"User with email '{request.Email}' is already exists.");
        }

        request.Password = Bcrypt.HashPassword(request.Password);

        User registeredUser = await _userRepository.AddAsync(_mapper.Map<User>(request));

        return _mapper.Map<UserResponse>(registeredUser);
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role)
        };

        string appSettingsToken = _configuration.GetSection("AppSettings:Token").Value
            ?? throw new Exception("AppSettings:Token is null");

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(60),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}