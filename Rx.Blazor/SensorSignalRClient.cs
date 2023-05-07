using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Rx.Blazor;

public class SensorSignalRClient : SignalRClientBase, ISensorSignalRClient
{
    public SensorSignalRClient(NavigationManager navigationManager)
        : base(navigationManager, "https://localhost:7109/SensorHub")
    {
    }

    public void Message(Action<string, string> action)
    {
        if (!Started)
        {
            HubConnection.On("SendMessage", action);
        }
    }

    public void MessageToCaller(Action<string, string> action)
    {
        if (!Started)
        {
            HubConnection.On(nameof(Message), action);
        }
    }

    public void MessageToGroup(Action<string, string> action)
    {
        if (!Started)
        {
            HubConnection.On(nameof(MessageToGroup), action);
        }
    }

    public async Task Message(string user, string message)
    {
        await HubConnection.SendAsync(nameof(Message), user,message);
    }

    public async Task MessageToCaller(string user, string message)
    {
        await HubConnection.SendAsync(nameof(MessageToCaller), user, message);
    }

    public async Task MessageToGroup(string user, string message)
    {
        await HubConnection.SendAsync(nameof(MessageToGroup), user, message);
    }
}