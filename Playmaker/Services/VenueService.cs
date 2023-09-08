using System.Net;
using AutoMapper;
using Playmaker.Dtos;
using Playmaker.Exceptions;
using Playmaker.Models;
using Playmaker.Repositories;

namespace Playmaker.Services;

public class VenueService : IVenueService
{
    private readonly IVenueRepository _venueRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public VenueService(IVenueRepository venueRepository, IUserRepository userRepository, IMapper mapper)
    {
        _venueRepository = venueRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<VenueResponse> AddAsync(int userId, VenueCreateRequest request)
    {
        if (!await _userRepository.ExistAsync(userId))
        {
            throw new ResponseException(HttpStatusCode.NotFound, $"User with id '{userId}' is not found.");
        }

        Venue venue = _mapper.Map<Venue>(request);
        venue.UserId = userId;

        Venue addedVenue = await _venueRepository.AddAsync(venue);

        return _mapper.Map<VenueResponse>(addedVenue);
    }

    public async Task<VenueResponse?> GetAsync(int venueId)
    {
        Venue? venue = await _venueRepository.GetAsync(venueId);

        if (venue is null)
        {
            throw new ResponseException(HttpStatusCode.NotFound, $"Venue with id '{venueId}' is not found.");
        }

        return _mapper.Map<VenueResponse>(venue);
    }
}