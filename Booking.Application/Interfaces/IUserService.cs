using Booking.Application.DTOs;

namespace Booking.Application.Interfaces.Services;

public interface IUserService
{
    public void CreateUser(CreateUserRequest createUser);
    Task<LoginUserResponse> LoginUser (LoginUserRequest loginUser);
    Task<IEnumerable<ProviderDTO>> GetProviders();
    Task<IEnumerable<ClientDTO>> GetClients();
}