using Microsoft.AspNetCore.SignalR;

namespace SignalDashBoard
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
      
        private readonly IHubContext<SensorHub,ISensorHub> _sensorHub;
        public Worker(ILogger<Worker> logger,  IHubContext<SensorHub, ISensorHub> sensorHub)
        {
            _logger = logger;
          
            _sensorHub = sensorHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
           
                
                await _sensorHub.Clients.All.SendMessage("benoitt", "msg");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}