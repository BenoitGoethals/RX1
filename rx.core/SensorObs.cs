using System.Collections.Concurrent;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using static rx.core.openweather.OpenWeatherHelper;

namespace rx.core;

public class SensorObs : IObservable<Measurement>, IDisposable, ISensorObs
{
    private readonly Random _randomInt = new();
    private readonly ISensorData _dataFunc;

    public SensorObs(string name,ISensorData sensorData)
    {
        Name = name;
        this._dataFunc= sensorData;
    }

    /// <summary>
    /// Default Data
    /// </summary>
    public SensorObs()
    {
        this.Name = "test"+ _randomInt.NextDouble();
        _dataFunc = new RandomData();
    }

   

    public bool IsRunning { get; private set; }
    private readonly List<IObserver<Measurement>>? _observers = new();
    private CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly ConcurrentBag<Measurement?> _measurements = new();

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

    public Guid Guid { get; } = Guid.NewGuid();
    public string Name { get; set; }

    private async Task RunSensor()
    {
        if (IsRunning) return;
        IsRunning = true;
        _cancellationToken = _cancellationTokenSource.Token;

        while (!_cancellationToken.IsCancellationRequested)
        {
            if (!IsRunning) continue;
            var delay = Task.Delay(5000, _cancellationToken);
            if (_observers != null)
                foreach (var observer in _observers.ToList())
                {
                    if (_dataFunc != null)
                    {
                        var mes = _dataFunc.GetData();

                        _measurements.Add(mes);
                        try
                        {
                            if (mes != null)
                            {
                                observer.OnNext(mes);
                            }
                        }
                        catch (Exception ex)
                        {
                            observer.OnError(ex);
                        }

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

