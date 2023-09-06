namespace Playmaker.Models;

public class Venue
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;

    public List<Field> Fields { get; set; } = new();

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}