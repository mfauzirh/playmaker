using Microsoft.EntityFrameworkCore;
using Playmaker.Models;

namespace Playmaker.Data;

public class DataContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Venue> Venues { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
         * User entity
        */
        modelBuilder.Entity<User>()
            .ToTable("users");

        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .HasColumnName("id");

        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .HasColumnName("name");

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasColumnName("email");

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .HasColumnName("password");

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasColumnName("role");

        // User One-to-Many with Venue
        modelBuilder.Entity<User>()
            .HasMany(u => u.Venues)
            .WithOne(v => v.User)
            .HasForeignKey(v => v.UserId)
            .IsRequired();

        // User One-to-Many with Booking
        modelBuilder.Entity<User>()
            .HasMany(u => u.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        /*
         * Venue entity
        */
        modelBuilder.Entity<Venue>()
            .ToTable("venues");

        modelBuilder.Entity<Venue>()
            .Property(v => v.Id)
            .HasColumnName("id");

        modelBuilder.Entity<Venue>()
            .Property(v => v.Name)
            .HasColumnName("name");

        modelBuilder.Entity<Venue>()
            .Property(v => v.Phone)
            .HasColumnName("phone");

        modelBuilder.Entity<Venue>()
            .Property(v => v.Address)
            .HasColumnName("address");

        modelBuilder.Entity<Venue>()
            .Property(v => v.UserId)
            .HasColumnName("user_id");

        // Venue One-To-Many with Fields
        modelBuilder.Entity<Venue>()
            .HasMany(v => v.Fields)
            .WithOne(f => f.Venue)
            .HasForeignKey(f => f.VenueId)
            .IsRequired();

        /*
         * Field entity
        */
        modelBuilder.Entity<Field>()
            .ToTable("fields");

        modelBuilder.Entity<Field>()
            .Property(f => f.Id)
            .HasColumnName("id");

        modelBuilder.Entity<Field>()
            .Property(f => f.Name)
            .HasColumnName("name");

        modelBuilder.Entity<Field>()
            .Property(f => f.Type)
            .HasColumnName("type");

        modelBuilder.Entity<Field>()
            .Property(f => f.VenueId)
            .HasColumnName("venue_id");

        // Field One-to-Many with Booking
        modelBuilder.Entity<Field>()
            .HasMany(f => f.Bookings)
            .WithOne(b => b.Field)
            .HasForeignKey(b => b.FieldId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        /*
         * Booking entity
        */
        modelBuilder.Entity<Booking>()
            .ToTable("bookings");

        modelBuilder.Entity<Booking>()
            .Property(b => b.Id)
            .HasColumnName("id");

        modelBuilder.Entity<Booking>()
            .Property(b => b.PlayDateStart)
            .HasColumnName("play_date_start");

        modelBuilder.Entity<Booking>()
            .Property(b => b.PlayDateEnd)
            .HasColumnName("play_date_end");

        modelBuilder.Entity<Booking>()
            .Property(b => b.UserId)
            .HasColumnName("user_id");

        modelBuilder.Entity<Booking>()
            .Property(b => b.FieldId)
            .HasColumnName("field_id");

        /*
         * UserBooking Junction Table
        */
        modelBuilder.Entity<UserBooking>()
            .ToTable("user_has_booking")
            .HasKey(ub => new { ub.UserId, ub.BookingId });

        modelBuilder.Entity<UserBooking>()
            .Property(ub => ub.UserId)
            .HasColumnName("user_id");

        modelBuilder.Entity<UserBooking>()
            .Property(ub => ub.BookingId)
            .HasColumnName("booking_id");

        modelBuilder.Entity<UserBooking>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBookings)
            .HasForeignKey(ub => ub.UserId);
        modelBuilder.Entity<UserBooking>()
            .HasOne(ub => ub.Booking)
            .WithMany(b => b.UserBookings)
            .HasForeignKey(ub => ub.BookingId);
    }
}