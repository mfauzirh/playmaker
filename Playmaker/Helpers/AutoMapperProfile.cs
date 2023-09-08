using AutoMapper;
using Playmaker.Dtos;
using Playmaker.Models;

namespace Playmaker.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<User, RegisterRequest>().ReverseMap();
        CreateMap<Venue, VenueResponse>().ReverseMap();
        CreateMap<Venue, VenueCreateRequest>().ReverseMap();
    }
}