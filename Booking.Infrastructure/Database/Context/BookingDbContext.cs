using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Database;

public class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<Domain.Entities.Booking> Bookings { get; set; }


    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);
        modelBuilder.Entity<User>();
        modelBuilder.Entity<Availability>();
        modelBuilder.Entity<Domain.Entities.Booking>();
    }
}