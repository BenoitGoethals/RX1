using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using rx.core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SignalDashBoard
{
    public class SensorHub:Hub<ISensorHub>
    {
        private readonly IServiceProvider _service;


        public SensorHub(IServiceProvider service)
        {
            this._service = service;
        }

        public async Task SendMessage(SensorData sensorData)
            => await Clients.All.SendMessage(sensorData);

        public async Task SendMessageToCaller(SensorData sensorData)
            => await Clients.Caller.SendMessageToCaller(sensorData);

        public async Task SendMessageToGroup(SensorData sensorData)
            => await Clients.All.SendMessageToGroup(sensorData);

        public async Task StartStop(string server)
        {

            var worker = _service.GetServices<IHostedService>().OfType<Worker>().Single();
            if (worker.Obs != null)
            {
                var obsServer = worker.Obs.First(obs => obs.Name == server);
                if (obsServer.IsRunning)
                {
                    obsServer.Stop();
                }
                else
                {
                     obsServer.Restart();
                }
            }
        }

    }
}
