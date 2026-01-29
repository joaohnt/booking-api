using System.Security.Cryptography;
using Bogus;
using Booking.Application.DTOs;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.Repositories;
using Booking.Domain.ValueObjects;
using Moq;
using Booking.Application.Services;


namespace Booking.Tests.Services;

public class UserServiceTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void TestRegisterUser_GivenAllParameters_ThenShouldCreate()
    {
        //arrange
        var command = new CreateUserCommand()
        {
        Name = _faker.Person.FullName,
        Email = _faker.Person.Email,
        Password = _faker.Random.AlphaNumeric(10),
        Role = _faker.PickRandom<Role>()
        };
        var repo = new Mock<IUserRepository>();
        var service = new UserService(repo.Object);
        
        //act
        service.CreateUser(command);
        
        //assert
        repo.Verify(r => r.Add(It.Is<User>(u =>
            u.Name == command.Name &&
            u.Email.Equals(Email.Create(command.Email)) &&
            u.PasswordHash.Contains("HASH") &&
            u.Role == command.Role
        )), Times.Once);
    }
    
}