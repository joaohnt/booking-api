using Booking.Domain.Entities;

namespace Booking.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}