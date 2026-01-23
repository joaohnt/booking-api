namespace Booking.Domain.ValueObjects;

public class TimeRange
{
    public DateTime Start { get; }
    public DateTime End { get; }

    internal TimeRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }
    
    public static TimeRange Create(DateTime start, DateTime end)
    {
        if (end <= start)
            throw new ArgumentException("O fim n pode ser antes do inicio");
        
        if(start < DateTime.Today)
            throw new ArgumentException("Nao e possivel agendar pra uma data passada");
        
        return new TimeRange(start, end);
    }
    
    public override bool Equals(object? obj)
        => obj is TimeRange other && Start == other.Start && End == other.End;
    
    public override int GetHashCode()
        => Start.GetHashCode()  ^ End.GetHashCode();
    
    public override string ToString()
        => $"{Start:HH:mm} - {End:HH:mm}";
}