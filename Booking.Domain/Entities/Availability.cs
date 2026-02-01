using System.Text.Json.Serialization;
using Booking.Domain.Enums;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Entities;

public class Availability
{

    public int Id { get; set; }
    public TimeRange TimeRange { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    
    [JsonIgnore]
    public User Provider { get; set; }
    public int ProviderId { get; set; }
    

    public void Cancel(int availabilityId)
    {
        if(Id != availabilityId)
            throw new ArgumentException("Id invalido");
        
        if (AvailabilityStatus == AvailabilityStatus.CLOSED)
            throw new Exception("Nao pode cancelar uma disponibilidade com um cliente");
    }

    public void Update(TimeRange timeRange, int providerId)
    {
        if(ProviderId != providerId)
            throw new ArgumentException("A disponibilidade pertence a outro provbedor");
        
        if(AvailabilityStatus == AvailabilityStatus.CLOSED) 
            throw new ArgumentException("nao e possivel alterar o horario da disponibilidade com agendamento confirmado");
        TimeRange = timeRange;
    }
    private Availability() {} //ef
    public Availability(int providerId, TimeRange timeRange)
    {
        ProviderId = providerId;
        TimeRange = timeRange;
        AvailabilityStatus = AvailabilityStatus.OPEN;
    }
}