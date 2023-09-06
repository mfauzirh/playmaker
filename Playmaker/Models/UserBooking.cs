namespace Playmaker.Models;

public class UserBooking
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
}