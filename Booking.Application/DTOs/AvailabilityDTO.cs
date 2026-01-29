using Booking.Domain.Enums;
using Booking.Domain.ValueObjects;

namespace Booking.Application.DTOs;

public class AvailabilityDTO
{
    public int Id { get; set; }
    public TimeRange TimeRange { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public int ProviderId { get; set; }
}