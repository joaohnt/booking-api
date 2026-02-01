using Booking.Application.DTOs;
using Booking.Domain.ValueObjects;

namespace Booking.Application.Interfaces.Services;

public interface IAvailabilityService
{
    Task<AvailabilityDTO> CreateAvailability(int providerId, CreateAvailabilityRequest createAvailability);
    Task<IEnumerable<AvailabilityDTO>> GetProviderAvailability(int providerId);
    Task RemoveAvailability(int availabilityId); 
    Task UpdateAvailability(int availabilityId, int providerId, TimeRange timeRange);

}