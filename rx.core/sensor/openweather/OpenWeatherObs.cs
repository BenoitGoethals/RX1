namespace rx.core.sensor.openweather;

public class OpenWeatherObs : ISensorObs
{
    public bool IsRunning { get; }
    public string Name { get; set; }
    public void Start()
    {
        throw new NotImplementedException();
    }

    public void Restart()
    {
        throw new NotImplementedException();
    }

    public IDisposable Subscribe(IObserver<Measurement> observer)
    {
        throw new NotImplementedException();
    }
}