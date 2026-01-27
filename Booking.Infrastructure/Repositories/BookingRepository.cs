using Booking.Domain.DTOs;
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
        return  _context.Bookings.Where(b => b.ClientId == clientId).Include(b => b.Availability).ToListAsync();
    }
}