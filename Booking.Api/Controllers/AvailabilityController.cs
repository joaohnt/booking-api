using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Booking.Application.DTOs;
using Booking.Application.Interfaces.Services;
using Booking.Application.Services;
using Booking.Domain.Enums;
using Booking.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("availability")]
public class AvailabilityController : ControllerBase
{
    private readonly IAvailabilityService _availabilityService;

    public AvailabilityController(IAvailabilityService availabilityService)
    {
        _availabilityService = availabilityService;
    }

    // CRIAR DISPONIVILIDADE
    [Authorize(Roles = nameof(Role.PROVIDER))]
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateAvailability([FromBody] CreateAvailabilityRequest request)
    {
        var providerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _availabilityService.CreateAvailability(providerId, request);

        return Ok(result);
    }
    
    // ATUALIZAR DISPONIBILIDADE
    [Authorize(Roles = nameof(Role.PROVIDER))]
    [HttpPut]
    [Route("{availabilityId:int}/update")]
    public async Task<IActionResult> UpdateAvailability([FromRoute] int availabilityId, [FromBody] UpdateAvailabilityRequest request)
    {
        var providerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var timeRange = TimeRange.Create(request.Start, request.End);
        await _availabilityService.UpdateAvailability(availabilityId, providerId, timeRange);

        return NoContent();
    }

    // VER DISPONIBILIDADES DE PROVEDOR X
    [Authorize]
    [HttpGet]
    [Route("{providerId:int}")]
    public async Task<IActionResult> GetProviderAvailability([FromRoute] int providerId)
    {
        var result = await _availabilityService.GetProviderAvailability(providerId);
        return Ok(result);
    }
    [Authorize(Roles = nameof(Role.PROVIDER))]
    [HttpDelete]
    [Route("{availabilityId:int}/cancel")]
    public async Task <IActionResult> CancelBooking([FromRoute] int availabilityId)
    {
        await _availabilityService.RemoveAvailability(availabilityId);
        return Ok("Disponibilidade cancelada");
    }
}
