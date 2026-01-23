using Bogus;
using Booking.Domain.ValueObjects;

namespace Booking.Tests.ValueObjects;

public class TimeRangeTests
{
    private readonly Faker _faker = new("pt_BR");
    
    [Fact]
    public void TestTimeRange_GivenAllParameters_ThenShouldCreateAvailabilityCorrectly()
    {
        //arrange
        var expectedStart = _faker.Date.Soon();
        var expectedEnd = _faker.Date.Soon().AddDays(7);

        //act
        var timeRange = new TimeRange(expectedStart, expectedEnd);

        //assert
        Assert.Equal(timeRange = new TimeRange(expectedStart, expectedEnd), timeRange);
    }
    
    [Fact]
    public void TestTimeRange_GivenEndDateInPast_ThenShouldntCreateTimeRange()
    {
        //arrange
        var expectedStart = _faker.Date.Past();
        var expectedEnd = _faker.Date.Future();
        
        //act
        var timeRange = new TimeRange(expectedStart, expectedEnd);
        
        //assert
        Assert.Throws<ArgumentException>(() => TimeRange.Create(expectedStart, expectedEnd));
    }
    
    [Fact]
    public void TestTimeRange_GivenEndBeforeStart_ThenShouldntCreateTimeRange()
    {
        //arrange
        var expectedStart = _faker.Date.Soon().AddDays(7);
        var expectedEnd = _faker.Date.Soon();
        
        //act
        var timeRange = new TimeRange(expectedStart, expectedEnd);
        
        //assert
        Assert.Throws<ArgumentException>(() => TimeRange.Create(expectedStart, expectedEnd));
    }
}