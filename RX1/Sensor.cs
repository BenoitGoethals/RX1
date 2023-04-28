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


        private async  Task<bool> RunSensor()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var delay = Task.Delay(500, _cancellationToken);
                var mes = new Measurement()
                {
                    Id = Guid.NewGuid(),
                    Temp = _randomInt.Next(60),
                    TimeCreated = DateTime.Now,
                    Humidity = _randomInt.Next(10,11),
                    WindSpeed = _randomInt.Next(5,8)

                };
                _measurements.Add(mes);
                SensorEvents?.Invoke(this, new SensorEventArgs() { MeasurementTaken = mes });
                await delay;

            }

            return true;
        }
        
        public  void Start()
        {

         
            var task = Task.Run(async () => { await RunSensor(); }, _cancellationToken);
            task.Wait(_cancellationToken);
        }

    }

    public class SensorEventArgs : EventArgs
    {
        public SensorEventArgs(Measurement measurementTaken)
        {
            MeasurementTaken = measurementTaken;
        }

        public Measurement MeasurementTaken { get; set; }
    }
}
