using System.Net;
using Microsoft.AspNetCore.Mvc;
using Playmaker.Dtos;
using Playmaker.Services;

namespace Playmaker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthServices _authServices;

    public AuthController(IAuthServices authServices)
    {
        _authServices = authServices;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        string token = await _authServices.Login(request);

        HttpContext.Response.Headers.Add("Authorization", $"Bearer {token}");

        return Ok();
    }

    [HttpPost("register")]
    public async Task<ActionResult<Response<UserResponse>>> Register(RegisterRequest request)
    {
        Response<UserResponse> response = new()
        {
            Data = await _authServices.Register(request)
        };

        return StatusCode((int)HttpStatusCode.Created, response);
    }
}