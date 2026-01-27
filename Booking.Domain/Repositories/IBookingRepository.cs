using Booking.Domain.DTOs;

namespace Booking.Domain.Repositories;

public interface IBookingRepository
{
    Task AddBooking(Entities.Booking booking);
    Task<List<Entities.Booking>> GetBookingsByClientId(int clientId);
}