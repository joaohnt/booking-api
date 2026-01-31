namespace Booking.Application.DTOs;

public class CreateAvailabilityRequest
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}