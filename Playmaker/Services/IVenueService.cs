using Playmaker.Dtos;

namespace Playmaker.Services;

public interface IVenueService
{
    Task<VenueResponse> AddAsync(int userId, VenueCreateRequest request);
    Task<VenueResponse?> GetAsync(int venueId);
}