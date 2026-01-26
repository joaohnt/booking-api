using Booking.Domain.Enums;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.DTOs;

public class AvailabilityDTO
{
    public TimeRange TimeRange { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public int ProviderId { get; set; }
}