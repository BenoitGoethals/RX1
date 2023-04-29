﻿using System.Collections.Concurrent;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading;

namespace RX1;

public class SensorObs : IObservable<Measurement>
{
    private readonly Random _randomInt = new();
    private bool _isRunning;
    private List<IObserver<Measurement>>? _observers=new List<IObserver<Measurement>>();
    private readonly CancellationToken _cancellationToken = new();
    private readonly ConcurrentBag<Measurement> _measurements = new();
    private async Task RunSensor()
    {
        if (_isRunning) return;
        _isRunning = true;

        while (_observers is {Count: > 0} && !_cancellationToken.IsCancellationRequested)
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var delay = Task.Delay(500, _cancellationToken);
                foreach (var observer in _observers.ToList())
                {
                    var mes = new Measurement()
                    {
                        Id = Guid.NewGuid(),
                        Temp = _randomInt.Next(60),
                        TimeCreated = DateTime.Now,
                        Humidity = _randomInt.Next(10, 11),
                        WindSpeed = _randomInt.Next(5, 8)
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
        }
       

        _isRunning = false;
        foreach (var observer in _observers.ToList())
        {
            observer.OnCompleted();
        }
    }
       

    public void Start(){
        
        var task = Task.Run(async () => { await RunSensor(); }, _cancellationToken);
        task.Wait(_cancellationToken);
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
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }

        return Disposable.Create(() => _observers.Remove(observer));
    }



    private class Unsubscriber : IDisposable
    {
        private List<IObserver<Measurement>> _observers;
        private IObserver<Measurement> _observer;

        public Unsubscriber(List<IObserver<Measurement>> observers, IObserver<Measurement> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}