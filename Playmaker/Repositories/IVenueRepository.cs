using Playmaker.Models;

namespace Playmaker.Repositories;

public interface IVenueRepository
{
    Task<Venue> AddAsync(Venue venue);
    Task<bool> ExistAsync(int venueId);
    Task<Venue?> GetAsync(int venueId);
}