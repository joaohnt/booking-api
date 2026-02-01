using Booking.Application.DTOs;

namespace Booking.Application.Interfaces.Services;

public interface IBookingService
{
    Task<BookingDTO> CreateBooking(int clientId, int availabilityId);
    Task<IEnumerable<BookingDTO>> GetBookingsByClientId(int clientId);
    Task<IEnumerable<BookingDTO>> GetBookingsFromProvider(int providerId);
    Task CancelBooking(int bookingId, int id);
    Task CancelBookingAsProvider(int bookingId, int providerId);
}