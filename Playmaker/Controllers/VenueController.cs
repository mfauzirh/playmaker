using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playmaker.Dtos;
using Playmaker.Services;

namespace Playmaker.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VenueController : ControllerBase
{
    private readonly IVenueService _venueService;

    public VenueController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    [Authorize(Roles = "owner")]
    [HttpPost]
    public async Task<ActionResult<Response<VenueResponse>>> Add(VenueCreateRequest request)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var response = new Response<VenueResponse>
        {
            Data = await _venueService.AddAsync(userId, request)
        };

        return StatusCode((int)HttpStatusCode.Created, response);
    }

    [HttpGet]
    public async Task<ActionResult<Response<VenueResponse>>> Get(int venueId)
    {
        var response = new Response<VenueResponse>
        {
            Data = await _venueService.GetAsync(venueId)
        };

        return Ok(response);
    }
}