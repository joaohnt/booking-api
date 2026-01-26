namespace Booking.Domain.Repositories;

public interface IBookingRepository
{
    Task AddBooking(Entities.Booking booking);
}