using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using rx.core;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace SignalDashBoard
{
    public class Worker : BackgroundService,IDisposable
    {
        private readonly ILogger<Worker> _logger;
        public ConcurrentBag<SensorObs>? Obs { get; private set; } = new ConcurrentBag<SensorObs>();


        private readonly IHubContext<SensorHub,ISensorHub> _sensorHub;
        public Worker(ILogger<Worker> logger,  IHubContext<SensorHub, ISensorHub> sensorHub)
        {
            _logger = logger;

            for (int i = 0; i < 10; i++)
            {
                Obs.Add(new SensorObs(){Name = i.ToString()});
            }
            _sensorHub = sensorHub;

            


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (Obs != null)
            {
                 foreach (var sensorOb in Obs)
                 {
                     sensorOb.Where(x => x.WindSpeed > 5)
                         .Subscribe(x =>
                         {
                             _sensorHub.Clients.All.SendMessage(new SensorData()
                                 { Humidy = x.Humidity, WindSpeed = x.WindSpeed, Sensor = sensorOb.Name, StatusServer = sensorOb.IsRunning,Temp = x.Temp});
                         });

                     


                     sensorOb.Start();

                     await Task.Delay(5000, stoppingToken);
                 }
            }
               

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
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