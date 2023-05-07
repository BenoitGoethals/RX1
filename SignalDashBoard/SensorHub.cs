using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalDashBoard
{
    public class SensorHub:Hub<ISensorHub>
    {
        public async Task SendMessage(string user, string message)
            => await Clients.All.SendMessage( user, message);

        public async Task SendMessageToCaller(string user, string message)
            => await Clients.Caller.SendMessageToCaller( user, message);

        public async Task SendMessageToGroup(string user, string message)
            => await Clients.All.SendMessageToGroup(  user, message);


    }
}
