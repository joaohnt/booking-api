using Booking.Domain.Repositories;
using Booking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly BookingDbContext  _context;
    public BookingRepository(BookingDbContext context)
    {
        _context = context;
    }

    public Task AddBooking(Domain.Entities.Booking booking)
    {
        _context.Bookings.AddAsync(booking);
        return _context.SaveChangesAsync();
    }

    public Task<List<Domain.Entities.Booking>> GetBookingsByClientId(int clientId)
    {
        return  _context.Bookings.AsNoTracking().Where(b => b.ClientId == clientId).Include(b => b.Availability).ToListAsync();
    }

    public Task<List<Domain.Entities.Booking>> GetBookingsFromProvider(int providerId)
    {
        return _context.Bookings
            .AsNoTracking().Include(b => b.Availability)
            .Where(b => b.Availability.ProviderId == providerId)
            .ToListAsync();
    }

    public Task<Domain.Entities.Booking?> GetBookingById(int bookingId)
    {
        return _context.Bookings.AsNoTracking().Where(b => b.Id == bookingId).Include(b => b.Availability).FirstOrDefaultAsync();
    }

    public Task RemoveBooking(Domain.Entities.Booking booking)
    {
        _context.Bookings.Remove(booking);
        return _context.SaveChangesAsync();
    }
}