using Microsoft.AspNetCore.SignalR;

namespace SignalDashBoard
{
    public class ClockHub : Hub<IClock>
    {
        public async Task SendTimeToClients(DateTime dateTime)
        {
            await Clients.All.ShowTime(dateTime);
        }
    }
}
