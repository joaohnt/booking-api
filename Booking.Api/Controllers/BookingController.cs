using System.Security.Claims;
using Booking.Application.Interfaces.Services;
using Booking.Application.Services;
using Booking.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [Authorize(Roles = nameof(Role.CLIENT))]
    [HttpPost]
    [Route("{availabilityId:int}/create")]
    public async Task<IActionResult> CreateBookingAsClient([FromRoute] int availabilityId)
    {
        var clientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _bookingService.CreateBooking(clientId, availabilityId);
        return Ok(result);
    }
    [Authorize]
    [HttpGet]
    [Route("/bookings")]
    public async Task<IActionResult> GetBookingsByClientId()
    {
        var clientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var bookings = await _bookingService.GetBookingsByClientId(clientId);
        return Ok(bookings);
    }
    
    [Authorize(Roles = nameof(Role.CLIENT))]
    [HttpDelete]
    [Route("{bookingId:int}/cancel")]
    public async Task <IActionResult> CancelBookingAsClient([FromRoute] int bookingId)
    {
        var clientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _bookingService.CancelBooking(bookingId, clientId);
        return Ok("Agendamento cancelado");
    }
    
    [Authorize(Roles = nameof(Role.PROVIDER))]
    [HttpDelete]
    [Route("{bookingId:int}/cancel/provider")]
    public async Task <IActionResult> CancelBookingAsProvider([FromRoute] int bookingId)
    {
        var providerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _bookingService.CancelBookingAsProvider(bookingId, providerId);
        return Ok("Agendamento cancelado");
    }
    
    [Authorize(Roles = nameof(Role.PROVIDER))]
    [HttpGet]
    [Route("/bookings/provider")]
    public async Task<IActionResult> GetBookingFromProvider([FromRoute] int providerId)
    {
        var pId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var bookings = await _bookingService.GetBookingsFromProvider(pId);
        return Ok(bookings);
    }
}