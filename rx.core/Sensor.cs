using System.Collections.Concurrent;

namespace rx.core
{
    public class Sensor
    {
        private readonly Random _randomInt=new();
        public event EventHandler<SensorEventArgs>? SensorEvents;
        private readonly CancellationToken _cancellationToken=new();
        private  readonly ConcurrentBag<Measurement> _measurements = new();

        public Sensor()
        {
            var task = Task.Run(async () =>
            {
                await RunSensor();

            }, _cancellationToken);
            task.Wait(_cancellationToken);
        }


        private async  Task RunSensor()

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


                SensorEvents?.Invoke(this, new SensorEventArgs { MeasurementTaken = mes });

                await delay;
            }
        }
        
        public  void Start()
        {
          
        }

    }

    public class SensorEventArgs : EventArgs
    {
      public Measurement MeasurementTaken { get; set; }
    }
}
