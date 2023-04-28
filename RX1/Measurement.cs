namespace RX1;

public class Measurement
{
    public DateTime TimeCreated { get; set; }

    public Guid Id { get; set; }

    public int Temp { get; set; }
    public int WindSpeed { get; set; }

    public int Humidity { get; set; }

    public override string ToString()
    {
        return $"{nameof(TimeCreated)}: {TimeCreated}, {nameof(Id)}: {Id}, {nameof(Temp)}: {Temp}";
    }
}