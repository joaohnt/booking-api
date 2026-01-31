using BCrypt;
using Booking.Application.DTOs;
using Booking.Application.Interfaces.Services;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Domain.ValueObjects;

namespace Booking.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    
    public void CreateUser(CreateUserRequest createUser)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUser.Password);
        
        var user = new User()
        {
            Name = createUser.Name,
            Email = Email.Create(createUser.Email),
            PasswordHash = hashedPassword,
        };
        
        _userRepository.Add(user);
    }

    public async Task<LoginUserResponse> LoginUser(LoginUserRequest request)
    {
        var user = await _userRepository.GetByEmail(request.Email);
        
        if (user == null)
            throw new Exception("email invalido");
        
        var password = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!password)
            throw new Exception("senha invalida");
        
        var token = _tokenService.GenerateToken(user);
        return new LoginUserResponse(user.Email.MailAdress, token);
    }

    public async Task<IEnumerable<ProviderDTO>> GetProviders()
    {
        var providers = await _userRepository.GetProviders();
        var response = providers.Select(p => new ProviderDTO()
        {
            Id = p.Id,
            Name = p.Name,
        });
        return response;
    }
    public async Task<IEnumerable<ClientDTO>> GetClients()
    {
        var clients = await _userRepository.GetClients();
        var response = clients.Select(p => new ClientDTO()
        {
            Id = p.Id,
            Name = p.Name,
        });
        return response;
    }
}