using Booking.Domain.Entities;

namespace Booking.Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}