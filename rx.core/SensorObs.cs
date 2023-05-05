using System.Collections.Concurrent;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace rx.core;

public class SensorObs : IObservable<Measurement>
{
    private readonly Random _randomInt = new();
    public bool IsRunning { get; private set; }
    private readonly List<IObserver<Measurement>>? _observers = new();
    private CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private readonly ConcurrentBag<Measurement> _measurements = new();
    private Task _task;


    private async Task RunSensor()
    {
        if (IsRunning) return;
        IsRunning = true;
        _cancellationToken = _cancellationTokenSource.Token;

        while (!_cancellationToken.IsCancellationRequested)
        {

            var delay = Task.Delay(500, _cancellationToken);
            if (_observers != null)
                foreach (var observer in _observers.ToList())
                {
                    var mes = new Measurement()
                    {
                        Id = Guid.NewGuid(),
                        Temp = _randomInt.Next(60),
                        TimeCreated = DateTime.Now,
                        Humidity = _randomInt.Next(10, 11),
                        WindSpeed = _randomInt.Next(0, 16)
                    };
                    _measurements.Add(mes);
                    try
                    {
                        observer.OnNext(mes);
                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                    }
                }

            await delay;

        }

        IsRunning = false;
        if (_observers != null)
            foreach (var observer in _observers.ToList())
            {
                observer.OnCompleted();
            }
    }


    public void Start()
    {
        _task = Task.Run(async () => { await RunSensor(); }, _cancellationToken);
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();

    }


    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(Measurement value)
    {
        throw new NotImplementedException();
    }

    public IDisposable Subscribe(IObserver<Measurement> observer)
    {
        if (_observers != null && !_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return Disposable.Create(() => _observers?.Remove(observer));
    }



}