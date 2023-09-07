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

    [HttpPatch("{userId}")]
    public async Task<ActionResult<Response<UserResponse>>> Update(int userId, UserUpdateRequest request)
    {
        var response = new Response<UserResponse>()
        {
            Data = await _userService.UpdateAsync(userId, request)
        };

        return Ok(response);
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult<Response<UserResponse>>> Update(int userId)
    {
        var response = new Response<UserResponse>()
        {
            Data = await _userService.DeleteAsync(userId)
        };

        return Ok(response);
    }
}