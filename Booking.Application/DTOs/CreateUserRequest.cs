using Booking.Domain.Enums;

namespace Booking.Application.DTOs;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}