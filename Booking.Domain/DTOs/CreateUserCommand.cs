using Booking.Domain.Enums;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.DTOs;

public class CreateUserCommand
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}