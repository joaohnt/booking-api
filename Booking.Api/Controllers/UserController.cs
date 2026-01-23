using Booking.Application.Services;
using Booking.Domain.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Services;
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
}