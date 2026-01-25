using Booking.Application.Services;
using Booking.Domain.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Services;
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

    [Authorize(Roles = nameof(Role.CLIENT))]
    [HttpGet]
    [Route("client")]
    public ActionResult TesteUser()
    {
        return Ok("voce eh usuario");
    }
    [Authorize(Roles = nameof(Role.PROVIDER))]
    [HttpGet]
    [Route("provider")]
    public ActionResult TesteProvider()
    {
        return Ok("voce eh proveor");
    }
}