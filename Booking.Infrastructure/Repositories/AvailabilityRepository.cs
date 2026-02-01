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

    public Task<bool> CheckAvailabilityConflict(int providerId, TimeRange timeRange)
    {
        return _context.Availabilities.AnyAsync(
            a => a.ProviderId == providerId 
                 && timeRange.Start < a.TimeRange.End
                 && timeRange.End > a.TimeRange.Start);
    }

    public Task<bool> CheckBookingAvailability(int Id)
    {
        return _context.Availabilities.AnyAsync(a => a.Id == Id && a.AvailabilityStatus == AvailabilityStatus.OPEN);
    }

    public Task<Availability?> GetById(int Id)
    {
        return _context.Availabilities.FirstOrDefaultAsync(a => a.Id == Id);
    }

    public Task<List<Availability>> GetProviderAvailability(int providerId)
    {
        return _context.Availabilities.Where(a => a.ProviderId == providerId).ToListAsync();
    }
    public Task UpdateStatus(Availability availability)
    {
        return _context.Availabilities.Where(a => a.Id == availability.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(
                a => a.AvailabilityStatus, AvailabilityStatus.OPEN));
    }

    public Task RemoveAvailability(Availability availability)
    {
        _context.Remove(availability);
        return _context.SaveChangesAsync();
    }
    
    public Task UpdateTimeRange(Availability availability)
    {
        return _context.Availabilities
            .Where(a => a.Id == availability.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.TimeRange.Start, availability.TimeRange.Start)
                .SetProperty(a => a.TimeRange.End, availability.TimeRange.End)
            );
    }
}