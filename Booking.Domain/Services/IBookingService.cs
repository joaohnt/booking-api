using Booking.Domain.DTOs;

namespace Booking.Domain.Services;

public interface IBookingService
{
    Task<BookingDTO> CreateBooking(int clientId, int availabilityId);
    Task<IEnumerable<Entities.Booking>> GetBookingsByClientId(int clientId);
    Task CancelBooking(int bookingId, int clientId);
}