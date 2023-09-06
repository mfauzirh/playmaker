namespace Playmaker.Models;

public class Field
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;

    public List<Booking> Bookings { get; set; } = new();

    public int VenueId { get; set; }
    public Venue Venue { get; set; } = null!;
}