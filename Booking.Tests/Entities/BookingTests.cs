using Bogus;
using Booking.Domain.Entities;
using Booking.Domain.Enums;
using Booking.Domain.ValueObjects;

namespace Booking.Tests.Entities;

public class BookingTests
{
    private readonly Faker _faker = new("pt_BR");

    [Fact]
    public void TestBook_WithOpenAvailability_ThenConfirm()
    {
        //arrange
        var availability = new Availability
        {
            Id = 1,
            AvailabilityStatus = AvailabilityStatus.OPEN
        };
        var booking = new Domain.Entities.Booking();
        
        //act
        booking.Book(availability, clientId: 2);

        //assert
        Assert.Equal(2, booking.ClientId);
        Assert.Equal(1, booking.AvailabilityId);
        Assert.Equal(AvailabilityStatus.CLOSED, availability.AvailabilityStatus);
    }
    
    // dps fazer todos os testes com theory e inlinedata
    [Fact]
    public void TestBook_GivenInvalidAvailabilityParameters_ThenShouldThrow()
    {
        //arrange
        var availability = new Availability
        {
            Id = 1,
            AvailabilityStatus = AvailabilityStatus.CLOSED
        };
        var booking = new Domain.Entities.Booking();
        

        //assert
        Assert.Throws<ArgumentException>(() => booking.Book(availability, clientId: 2));
    }
}