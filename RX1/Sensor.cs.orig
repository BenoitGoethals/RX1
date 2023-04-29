using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RX1
{
    public class Sensor
    {
        private readonly Random _randomInt=new();
        public event EventHandler<SensorEventArgs>? SensorEvents;
        private readonly CancellationToken _cancellationToken=new();
        private  readonly ConcurrentBag<Measurement> _measurements = new();
<<<<<<< HEAD


        private async  Task RunSensor()
=======
        
        private async  Task<bool> RunSensor()
>>>>>>> 05e058d2b8b3b975c9b2b45cbe4be8d528792227
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var delay = Task.Delay(500, _cancellationToken);
                var mes = new Measurement
                {
                    Id = Guid.NewGuid(),
                    Temp = _randomInt.Next(60),
                    TimeCreated = DateTime.Now,
                    Humidity = _randomInt.Next(10,11),  
                    WindSpeed = _randomInt.Next(5,8)
                };
                _measurements.Add(mes);
<<<<<<< HEAD
                SensorEvents?.Invoke(this, new SensorEventArgs(measurementTaken: mes));
=======
                SensorEvents?.Invoke(this, new SensorEventArgs { MeasurementTaken = mes });
>>>>>>> 05e058d2b8b3b975c9b2b45cbe4be8d528792227
                await delay;
            }
        }
        
        public  void Start()
        {
            var task = Task.Run(async () => { await RunSensor(); }, _cancellationToken);
            task.Wait(_cancellationToken);
        }

    }

    public class SensorEventArgs : EventArgs
    {
      public Measurement MeasurementTaken { get; set; }
    }
}
