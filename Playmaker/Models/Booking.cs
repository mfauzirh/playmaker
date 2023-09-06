namespace Playmaker.Models;

public class Booking
{
    public int Id { get; set; }
    public DateTime PlayDateStart { get; set; }
    public DateTime PlayDateEnd { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int FieldId { get; set; }
    public Field Field { get; set; } = null!;

    public List<UserBooking> UserBookings { get; set; } = new();
}