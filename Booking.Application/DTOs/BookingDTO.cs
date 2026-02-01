using Booking.Domain.Entities;

namespace Booking.Application.DTOs;

public class BookingDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ProviderName { get; set; }
    public string? ClientName { get; set; }
    public Availability Availability { get; set; }
}