using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Repositories;
using Booking.Domain.ValueObjects;
using Booking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class AvailabilityRepository : IAvailabilityRepository
{
    private readonly BookingDbContext  _context;
    public AvailabilityRepository(BookingDbContext context)
    {
        _context = context;
    }
    
    
    public Task AddAvailability(Availability availability)
    {
        _context.Availabilities.Add(availability);
        return _context.SaveChangesAsync();
    }

    public Task<bool> CheckAvailability(int providerId, TimeRange timeRange)
    {
        return _context.Availabilities.AnyAsync(
            a => a.ProviderId == providerId 
                 && timeRange.Start < a.TimeRange.End
                 && timeRange.End > a.TimeRange.Start);
    }

    public Task GetAvailability(int providerId)
    {
        throw new NotImplementedException();
    }
}