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
        
        var booking = new Domain.Entities.Booking(clientId, availabilityId, DateTime.UtcNow);
        booking.Book(availability, clientId);
        
        await _bookingRepository.AddBooking(booking);
        
        return new BookingDTO()
        {
            Id = booking.Id,
            CreatedAt = DateTime.UtcNow.Date,
        };
    }
    public async Task<IEnumerable<Domain.Entities.Booking>> GetBookingsByClientId(int clientId)
    {
        var result = await _bookingRepository.GetBookingsByClientId(clientId);
        return result;
    }

    public async Task CancelBooking(int bookingId, int clientId)
    {
        var booking = await _bookingRepository.GetBookingById(bookingId);
        if (booking == null)
            throw new ArgumentException("Agendamento nao encontrado");
        
        var availability = await _availabilityRepository.GetById(booking.AvailabilityId);
        if (availability == null)
            throw new ArgumentException("Disponibilidade nao encontrada");
        
        booking.Cancel(availability, clientId);
        
        await _availabilityRepository.UpdateStatus(availability);
        await _bookingRepository.RemoveBooking(booking);
    }
}