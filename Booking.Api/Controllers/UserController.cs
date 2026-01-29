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
    
    [HttpPost]
    [Route("signup")]
    public ActionResult SignUp([FromBody] CreateUserCommand command)
    {
        _userService.CreateUser(command);
        return Ok(command);
    }

    [HttpPost]
    [Route("signin")]
    public async Task<LoginUserResult> SignIn([FromBody] LoginUserCommand command)
    {
        var result = await _userService.LoginUser(command);
        return result;
    }

    [HttpGet]
    [Route("providers")]
    public async Task<IActionResult> GetProviders()
    {
        var providers = await _userService.GetProviders();
        return Ok(providers);
    }
    [HttpGet]
    [Route("clients")]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _userService.GetClients();
        return Ok(clients);
    }
}