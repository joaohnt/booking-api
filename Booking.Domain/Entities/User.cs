using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Booking.Domain.Enums;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Entities;

public class User // (provider/client)
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Email Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; init; } = Role.CLIENT;
    
    public ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}