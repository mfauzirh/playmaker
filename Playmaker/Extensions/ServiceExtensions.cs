using Playmaker.Repositories;
using Playmaker.Services;

namespace Playmaker.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServiceServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthServices, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVenueService, VenueService>();

        return services;
    }
}