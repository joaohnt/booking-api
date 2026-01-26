using Booking.Domain.DTOs;

namespace Booking.Domain.Services;

public interface IBookingService
{
    Task<BookingDTO> CreateBooking(int clientId, int availabilityId);
}