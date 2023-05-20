namespace rx.core.sensor;

public class RandomData : ISensorData
{
    private readonly Random _randomInt = new();
    public Measurement? GetData()
    {
        var mes = new Measurement()
        {
            Id = Guid.NewGuid(),
            Temp = _randomInt.Next(60),
            TimeCreated = DateTime.Now,
            Humidity = _randomInt.Next(0, 11),
            WindSpeed = _randomInt.Next(0, 16)
        };
        return mes;
    }
}