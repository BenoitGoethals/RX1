using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using rx.core;

namespace SignalDashBoard
{
    public class SensorHub:Hub<ISensorHub>
    {
        public async Task SendMessage(SensorData sensorData)
            => await Clients.All.SendMessage(sensorData);

        public async Task SendMessageToCaller(SensorData sensorData)
            => await Clients.Caller.SendMessageToCaller(sensorData);

        public async Task SendMessageToGroup(SensorData sensorData)
            => await Clients.All.SendMessageToGroup(sensorData);


    }
}
