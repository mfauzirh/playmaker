namespace Playmaker.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;

    public List<Venue> Venues { get; set; } = new();

    public List<Booking> Bookings { get; set; } = new();

    public List<UserBooking> UserBookings { get; set; } = new();
}