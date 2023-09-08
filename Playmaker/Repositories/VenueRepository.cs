using Microsoft.EntityFrameworkCore;
using Playmaker.Data;
using Playmaker.Models;

namespace Playmaker.Repositories;

public class VenueRepository : IVenueRepository
{
    private readonly DataContext _context;

    public VenueRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Venue> AddAsync(Venue venue)
    {
        await _context.Venues.AddAsync(venue);
        await _context.SaveChangesAsync();
        return venue;
    }

    public async Task<bool> ExistAsync(int venueId) => await _context.Venues.AnyAsync(x => x.Id == venueId);

    public async Task<Venue?> GetAsync(int venueId) => await _context.Venues.FindAsync(venueId);
}