using Booking.Domain.DTOs;
using Booking.Domain.Entities;

namespace Booking.Domain.Services;

public interface IAvailabilityService
{
    Task<AvailabilityDTO> CreateAvailability(int providerId, CreateAvailabilityCommand createAvailability);
}