using Booking.Domain.Enums;

namespace Booking.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CanceledAt { get; set; }
    public string? CancellationReason { get; set; }
    public BookingStatus BookingStatus { get; set; } = BookingStatus.PENDING;
    
    
    
    public Availability Availability { get; set; }
    public int AvailabilityId { get; set; }
    
    
    public User Client { get; set; }
    public int ClientId { get; set; }


    public void Book(Availability availability, int clientId)
    {
        if(availability.AvailabilityStatus == AvailabilityStatus.CLOSED)
            throw new ArgumentException($"nao ta disponivel");
        if(BookingStatus == BookingStatus.CANCELLED || BookingStatus == BookingStatus.CONFIRMED)
            throw new ArgumentException("a reserva ja esta cancelada/confirmada");
        
        ClientId = clientId;
        AvailabilityId = availability.Id;
        availability.AvailabilityStatus = AvailabilityStatus.CLOSED;
        BookingStatus = BookingStatus.CONFIRMED;
    }

    // pra eu cancelar, EU tenho que ter feito a reserva, eu n posso cancelar qlqr coisa
    // n pode cancelar se ja tiver cancelado
    // atualiza os status que podem ser nulos e reabre Availability
    public void Cancel(Availability availability, int clientId, string? reason)
    {
        if(ClientId != clientId) 
            throw new ArgumentException($"vc n pode cancelar a reserva de outro cliente");
        
        if(BookingStatus == BookingStatus.CANCELLED)
            throw new ArgumentException($"reserva ja cancelada");
        
        if (availability.Id != AvailabilityId)
            throw new ArgumentException("disponibilidade nao pertence a esta reserva");
        
        ClientId = clientId;
        AvailabilityId = availability.Id;
        availability.AvailabilityStatus = AvailabilityStatus.OPEN;
        BookingStatus = BookingStatus.CANCELLED;
        CanceledAt = DateTime.UtcNow;
        CancellationReason = reason;
    }
}