using Booking.Application.DTOs;

namespace Booking.Application.Interfaces.Services;

public interface IUserService
{
    public void CreateUser(CreateUserCommand createUser);
    Task<LoginUserResult> LoginUser (LoginUserCommand loginUser);
    Task<IEnumerable<ProviderDTO>> GetProviders();
    Task<IEnumerable<ClientDTO>> GetClients();
}