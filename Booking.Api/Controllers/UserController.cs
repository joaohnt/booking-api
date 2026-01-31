using Booking.Application.DTOs;
using Booking.Application.Interfaces.Services;
using Booking.Application.Services;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    
    // alterei p criar sempre client. caso o usuario queira ser provider,
    // tera q entrar em contato c suporte e realizar isso manualmente no banco
    // pois provider tem acesso a dados sensiveis (cancelar agendamento)
    [HttpPost]
    [Route("signup")]
    public ActionResult SignUp([FromBody] CreateUserRequest request)
    {
        _userService.CreateUser(request);
        return Ok(request);
    }
    
    [HttpPost]
    [Route("signin")]
    public async Task<LoginUserResponse> SignIn([FromBody] LoginUserRequest request)
    {
        var result = await _userService.LoginUser(request);
        return result;
    }

    [HttpGet]
    [Route("providers")]
    public async Task<IActionResult> GetProviders()
    {
        var providers = await _userService.GetProviders();
        return Ok(providers);
    }
    
    [Authorize(Roles = nameof(Role.PROVIDER))]
    [HttpGet]
    [Route("clients")]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _userService.GetClients();
        return Ok(clients);
    }
}