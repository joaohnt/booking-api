using Booking.Domain.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Repositories;
using Booking.Domain.Services;
using Booking.Domain.ValueObjects;

namespace Booking.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public void CreateUser(CreateUserCommand createUser)
    {
        var hash = createUser.Password + "HASH";
        
        var user = new User()
        {
            Name = createUser.Name,
            Email = Email.Create(createUser.Email),
            PasswordHash = hash,
            Role = createUser.Role
        };
        
        _userRepository.Add(user);
    }
}