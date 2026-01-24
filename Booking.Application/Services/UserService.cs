using BCrypt;
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
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUser.Password);
        
        var user = new User()
        {
            Name = createUser.Name,
            Email = Email.Create(createUser.Email),
            PasswordHash = hashedPassword,
            Role = createUser.Role
        };
        
        _userRepository.Add(user);
    }

    public async Task<LoginUserResult> LoginUser(LoginUserCommand command)
    {
        var user = await _userRepository.GetByEmail(command.Email);
        if (user == null)
            throw new Exception("emal invalido");
        var password = BCrypt.Net.BCrypt.Verify(command.Password, user.PasswordHash);
        if (!password)
            throw new Exception("senha invalida");
        var token = "tokenzudo";
        return new LoginUserResult(user.Email.MailAdress, token);
    }
}