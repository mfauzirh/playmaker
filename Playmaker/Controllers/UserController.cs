using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmaker.Dtos;
using Playmaker.Services;

namespace Playmaker.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<Response<UserResponse>>> Get(int userId)
    {
        var response = new Response<UserResponse>()
        {
            Data = await _userService.GetAsync(userId)
        };

        return Ok(response);
    }
}