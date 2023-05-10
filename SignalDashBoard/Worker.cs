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
        private ConcurrentBag<SensorObs>? _sensorObs=new ConcurrentBag<SensorObs>();


        private readonly IHubContext<SensorHub,ISensorHub> _sensorHub;
        public Worker(ILogger<Worker> logger,  IHubContext<SensorHub, ISensorHub> sensorHub)
        {
            _logger = logger;

            for (int i = 0; i < 10; i++)
            {
                _sensorObs.Add(new SensorObs(){Name = i.ToString()});
            }
            _sensorHub = sensorHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_sensorObs != null)
            {
                 foreach (var sensorOb in _sensorObs)
                 {
                     sensorOb.Where(x => x.WindSpeed > 5)
                         .Subscribe(x =>
                         {
                             _sensorHub.Clients.All.SendMessage(new SensorData()
                                 { Humidy = x.Humidity, WindSpeed = x.WindSpeed, Sensor = sensorOb.Name });
                         });

                     sensorOb
                         .Subscribe(xt =>
                         {
                             _sensorHub.Clients.All.SendMessage(new SensorData()
                                 { Humidy = xt.Humidity, WindSpeed = xt.WindSpeed, Sensor = sensorOb.Name });
                         });
                      
                     sensorOb.Start();

                     await Task.Delay(1000, stoppingToken);
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
            if (_sensorObs != null)
                foreach (var disp2 in _sensorObs)
                {
                    disp2?.Dispose();
                }

            _sensorObs = null;
        }
    }
}