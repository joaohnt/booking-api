using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Repositories;

public interface IAvailabilityRepository
{
    Task AddAvailability(Availability availability);
    Task<bool> CheckAvailability(int providerId, TimeRange timeRange);
    Task GetAvailability(int providerId);
}