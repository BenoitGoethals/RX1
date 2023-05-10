using System.Collections.Concurrent;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace rx.core;

public class SensorObs : IObservable<Measurement>, IDisposable
{
    private readonly Random _randomInt = new();

    public bool IsRunning { get; private set; }
    private readonly List<IObserver<Measurement>>? _observers = new();
    private CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly ConcurrentBag<Measurement> _measurements = new();

    protected bool Equals(SensorObs other)
    {
        return Guid.Equals(other.Guid);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SensorObs)obj);
    }

    public override int GetHashCode()
    {
        return Guid.GetHashCode();
    }

    public Guid Guid { get; set; } = Guid.NewGuid();
    public string Name { get; set; }

    private async Task RunSensor()
    {
        if (IsRunning) return;
        IsRunning = true;
        _cancellationToken = _cancellationTokenSource.Token;

        while (!_cancellationToken.IsCancellationRequested)
        {
            if (!IsRunning) continue;
            var delay = Task.Delay(500, _cancellationToken);
            if (_observers != null)
                foreach (var observer in _observers.ToList())
                {
                    var mes = new Measurement()
                    {
                        Id = Guid.NewGuid(),
                        Temp = _randomInt.Next(60),
                        TimeCreated = DateTime.Now,
                        Humidity = _randomInt.Next(0, 11),
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
        //if (_observers != null)
        //    foreach (var observer in _observers.ToList())
        //    {
        //        observer.OnCompleted();
        //    }
    }


    public void Start()
    {
        Task.Run(async () => { await RunSensor(); }, _cancellationToken);
    }

    public void Restart()
    {
        IsRunning = true;
    }

    public void Stop()
    {
        IsRunning = false;
        
    }



    public IDisposable Subscribe(IObserver<Measurement> observer)
    {
        if (_observers != null && !_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return Disposable.Create(() => _observers?.Remove(observer));
    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

