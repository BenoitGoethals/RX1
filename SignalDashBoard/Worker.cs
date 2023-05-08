using Microsoft.AspNetCore.SignalR;
using rx.core;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace SignalDashBoard
{
    public class Worker : BackgroundService,IDisposable
    {
        private readonly ILogger<Worker> _logger;
        private SensorObs? _sensorObs;


        private IDisposable? _disp;
        private IDisposable? _disp2;
        private readonly IHubContext<SensorHub,ISensorHub> _sensorHub;
        public Worker(ILogger<Worker> logger,  IHubContext<SensorHub, ISensorHub> sensorHub,SensorObs? sensorObs)
        {
            _logger = logger;
            _sensorObs=sensorObs;
            _sensorHub = sensorHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _disp = _sensorObs.Where(x => x.WindSpeed > 5)
                .Subscribe(x =>
                {
                 
                     _sensorHub.Clients.All.SendMessage("benoitt", "msg temp "+ x.Temp);

                });

            _disp2 = _sensorObs
                .Subscribe(x =>
                {
                    
                    _sensorHub.Clients.All.SendMessage("benoitt", "msg wind " +  x.WindSpeed);

                });
            _sensorObs.Start(); 

           await Task.Delay(1000, stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
           
                
              
                await Task.Delay(1000, stoppingToken);
            }
        }
        void IDisposable.Dispose()
        {
            _disp2?.Dispose();
            _disp?.Dispose();
            _sensorObs = null;
        }
    }
}