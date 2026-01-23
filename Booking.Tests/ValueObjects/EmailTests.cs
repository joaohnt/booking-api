using Bogus;
using Booking.Domain.ValueObjects;

namespace Booking.Tests.ValueObjects;
using Xunit;

public class EmailTests
{
    private readonly Faker _faker = new("pt_BR");
    
    
    [Fact]
    public void TestEmail_GivenAllParameters_ThenShouldCreateEmailCorrectly()
    {
        //arrange
        var expectedEmail = _faker.Person.Email;

        //act
        var email = new Email(expectedEmail);

        //assert
        Assert.Equal(expectedEmail, email.ToString());
    }
    
    [Fact]
    public void TestEmail_GivenWrongParameters_ThenShouldntCreateEmail()
    {
        //arrange
        var expectedEmail = "joaogmailcom";
        
        //assert
        Assert.Throws<ArgumentException>(() => Email.Create(expectedEmail));
    }
    
}