using Booking.Domain.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Repositories;
using Booking.Domain.Services;
using Booking.Domain.ValueObjects;

namespace Booking.Application.Services;

public class AvailabilityService : IAvailabilityService
{
    private  readonly IAvailabilityRepository _availabilityRepository;
    public AvailabilityService(IAvailabilityRepository availabilityRepository)
    {
        _availabilityRepository = availabilityRepository;
    }

    public async Task<AvailabilityDTO> CreateAvailability(int providerId, CreateAvailabilityCommand command)
    {
        var timeRange = TimeRange.Create(command.Start, command.End);

        var hasConflict = await _availabilityRepository.CheckAvailabilityConflict(providerId, timeRange);
        if (hasConflict)
            throw new ArgumentException("conflito de horario");

        var availability = new Availability(providerId, timeRange);
        await _availabilityRepository.AddAvailability(availability);

        return new AvailabilityDTO()
        {
            Id = availability.Id,
            TimeRange = availability.TimeRange,
            AvailabilityStatus = availability.AvailabilityStatus,
            ProviderId = providerId,
        };
    }

    public async Task<IEnumerable<AvailabilityDTO>> GetProviderAvailability(int providerId)
    {
        var availabilities = await _availabilityRepository.GetProviderAvailability(providerId);
        var response = availabilities.Select(a => new AvailabilityDTO
        {
            Id = a.Id,
            TimeRange = a.TimeRange,
            AvailabilityStatus = a.AvailabilityStatus,
            ProviderId = a.ProviderId
        });
        return response;
    }
}