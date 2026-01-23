using Booking.Domain.Enums;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Entities;

public class Availability
{
    public int Id { get; set; }
    public TimeRange TimeRange { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    
    public User Provider { get; set; }
    public int ProviderId { get; set; }
}