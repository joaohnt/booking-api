using Booking.Domain.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Repositories;

public interface IAvailabilityRepository
{
    Task AddAvailability(Availability availability);
    Task<bool> CheckAvailabilityConflict(int providerId, TimeRange timeRange);
    Task<bool> CheckBookingAvailability(int Id);
    Task<Availability?> GetById(int Id);
    Task<List<Availability>> GetProviderAvailability(int providerId);
    Task UpdateStatus(Availability availability);
    Task RemoveAvailability(Availability availability);
}