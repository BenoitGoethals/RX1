using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using rx.core.openweather;
using rx.core.sensor;
using rx.core.sensor.openweather;

namespace SignalDashBoard
{
    public class Worker : BackgroundService, IDisposable
    {
        private readonly ILogger<Worker> _logger;
        public ConcurrentBag<SensorObs>? Obs { get; private set; } = new();

        private readonly IHubContext<SensorHub, ISensorHub> _sensorHub;
        public Worker(ILogger<Worker> logger, IHubContext<SensorHub, ISensorHub> sensorHub)
        {
            _logger = logger;
            for (var i = 0; i < 10; i++)
            {
                Obs?.Add(new SensorObs());
            };

            foreach (var loc in OpenWeatherHelper.GetCities())
            {
                Obs?.Add(new SensorObs(loc.CityName, new OpenWeatherData(loc)));
            }
            _sensorHub = sensorHub;

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (Obs != null)
            {
                foreach (var sensorOb in Obs)
                {
                    sensorOb
                        .Subscribe(x =>
                        {
                            _sensorHub.Clients.All.Message(new SensorData()
                            { Humidy = x.Humidity, WindSpeed = x.WindSpeed, Sensor = sensorOb.Name, StatusServer = sensorOb.IsRunning, Temp = x.Temp });
                        });
                    sensorOb.Start();

                }
            }


            while (!stoppingToken.IsCancellationRequested)
            {

                await Task.Delay(1000, stoppingToken);
            }
        }
        void IDisposable.Dispose()
        {
            if (Obs != null)
                foreach (var disp2 in Obs)
                {
                    disp2?.Dispose();
                }

            Obs = null;
        }

        public object Stop(string server)
        {
            throw new NotImplementedException();
        }
    }
}