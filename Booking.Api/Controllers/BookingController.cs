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
    public async Task<IActionResult> CreateBooking([FromRoute] int availabilityId)
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
    public async Task <IActionResult> CancelBooking([FromRoute] int bookingId)
    {
        var clientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _bookingService.CancelBooking(bookingId, clientId);
        return Ok("Agendamento cancelado");
    }
}