namespace RX1;

public class Messarement
{
    public DateTime TimeCreated { get; set; }

    public Guid Id { get; set; }

    public int Temp { get; set; }

    public override string ToString()
    {
        return $"{nameof(TimeCreated)}: {TimeCreated}, {nameof(Id)}: {Id}, {nameof(Temp)}: {Temp}";
    }
}