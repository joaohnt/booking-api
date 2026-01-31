using Booking.Domain.Enums;

namespace Booking.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    // public DateTime? CanceledAt { get; set; }
    // public string? CancellationReason { get; set; }
    
    
    public Availability Availability { get; set; }
    public int AvailabilityId { get; set; }
    
    
    public User Client { get; set; }
    public int ClientId { get; set; }


    public void Book(Availability availability, int clientId)
    {
        if(availability.AvailabilityStatus == AvailabilityStatus.CLOSED)
            throw new ArgumentException($"nao ta disponivel");
        
        ClientId = clientId;
        AvailabilityId = availability.Id;
        availability.AvailabilityStatus = AvailabilityStatus.CLOSED;
    }

    // pra eu cancelar, EU tenho que ter feito a reserva, eu n posso cancelar qlqr coisa
    // n pode cancelar se ja tiver cancelado
    // atualiza os status que podem ser nulos e reabre Availability
    public void Cancel(Availability availability, int clientId)
    {
        if(ClientId != clientId) 
            throw new ArgumentException($"vc n pode cancelar a reserva de outro cliente");
        
        if (availability.Id != AvailabilityId)
            throw new ArgumentException("disponibilidade nao pertence a esta reserva");
        // verifica se faltam 2h pra o agendamento
        if (availability.TimeRange.Start - DateTime.UtcNow <= TimeSpan.FromHours(2) &&
            availability.TimeRange.Start > DateTime.UtcNow)
            throw new ArgumentException("voce nao pode cancelar um agendamento faltando 2h ou menos");
        
        availability.AvailabilityStatus = AvailabilityStatus.OPEN;
    }
    
    public void CancelByProvider(Availability availability, int providerId)
    {
        if (availability.ProviderId != providerId)
            throw new ArgumentException("vc n pode cancelar a reserva de outro provider");
        if (availability.Id != AvailabilityId)
            throw new ArgumentException("disponibilidade nao pertence a esta reserva");
        if (availability.TimeRange.Start - DateTime.UtcNow <= TimeSpan.FromHours(2) &&
            availability.TimeRange.Start > DateTime.UtcNow)
            throw new ArgumentException("voce nao pode cancelar um agendamento faltando 2h ou menos");

        availability.AvailabilityStatus = AvailabilityStatus.OPEN;
    }

    public Booking() { }
    public Booking(int clientId, int availabilityId, DateTime createdAt)
    {
        ClientId = clientId;
        AvailabilityId = availabilityId;
        CreatedAt = createdAt;
    }
}