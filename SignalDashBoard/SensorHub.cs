using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using rx.core.sensor;

namespace SignalDashBoard
{
    public class SensorHub:Hub<ISensorHub>
    {
        private readonly IServiceProvider _service;


        public SensorHub(IServiceProvider service)
        {
            this._service = service;
        }

        public async Task Message(SensorData sensorData)
            => await Clients.All.Message(sensorData);

        public async Task MessageToCaller(SensorData sensorData)
            => await Clients.Caller.MessageToCaller(sensorData);

        public async Task MessageToGroup(SensorData sensorData)
            => await Clients.All.MessageToGroup(sensorData);

        public async Task StartStopSensor(string server)
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
