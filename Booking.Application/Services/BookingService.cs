using System.Runtime.InteropServices.JavaScript;
using Booking.Domain.DTOs;
using Booking.Domain.Enums;
using Booking.Domain.Repositories;
using Booking.Domain.Services;

namespace Booking.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IAvailabilityRepository _availabilityRepository;
    public BookingService(IBookingRepository bookingRepository, IAvailabilityRepository availabilityRepository)
    {
        _bookingRepository = bookingRepository;
        _availabilityRepository = availabilityRepository;
    }

    public async Task<BookingDTO> CreateBooking(int clientId, int availabilityId)
    {
        var hasAvailability = await _availabilityRepository.CheckBookingAvailability(availabilityId);
        var availability = await _availabilityRepository.GetById(availabilityId);

        if (!hasAvailability)
            throw new ArgumentException("Availability not found");
        
        availability.AvailabilityStatus = AvailabilityStatus.CLOSED;
        
        var booking = new Domain.Entities.Booking(clientId, availabilityId);
        await _bookingRepository.AddBooking(booking);
        

        return new BookingDTO()
        {
            Id = booking.Id,
            CreatedAt = DateTime.UtcNow.Date,
        };
    }
}