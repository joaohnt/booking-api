using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Booking.Domain.DTOs;
using Booking.Domain.Enums;
using Booking.Domain.Services;
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
    public async Task<IActionResult> CreateAvailability([FromBody] CreateAvailabilityCommand command)
    {
        var providerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _availabilityService.CreateAvailability(providerId, command);

        return Ok(result);
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
}
