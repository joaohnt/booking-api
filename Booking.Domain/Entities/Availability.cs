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
    

    public void Cancel(int availabilityId)
    {
        if(Id != availabilityId)
            throw new ArgumentException("Id invalido");
        
        if (AvailabilityStatus == AvailabilityStatus.CLOSED)
            throw new Exception("Nao pode cancelar uma disponibilidade com um cliente");
    }
    private Availability() {} //ef
    public Availability(int providerId, TimeRange timeRange)
    {
        ProviderId = providerId;
        TimeRange = timeRange;
        AvailabilityStatus = AvailabilityStatus.OPEN;
    }
}