using Booking.Domain.DTOs;
using Booking.Domain.Entities;

namespace Booking.Domain.Services;

public interface IUserService
{
    public void CreateUser(CreateUserCommand createUser);
    Task<LoginUserResult> LoginUser (LoginUserCommand loginUser);
    Task<IEnumerable<ProviderDTO>> GetProviders();
}