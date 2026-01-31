namespace Booking.Domain.Repositories;

public interface IBookingRepository
{
    Task AddBooking(Entities.Booking booking);
    Task<List<Entities.Booking>> GetBookingsByClientId(int clientId);
    Task<List<Entities.Booking>> GetBookingsFromProvider(int providerId);
    Task<Entities.Booking?> GetBookingById(int bookingId);
    Task RemoveBooking(Entities.Booking booking);
}
