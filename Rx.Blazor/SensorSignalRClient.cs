using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using rx.core;

namespace Rx.Blazor;

public class SensorSignalRClient : SignalRClientBase, ISensorSignalRClient
{
    public SensorSignalRClient(NavigationManager navigationManager)
        : base(navigationManager, "https://localhost:7109/SensorHub")
    {
    }

    public void Message(Action<SensorData> action)
    {
        if (!Started)
        {
            HubConnection.On("SendMessage", action);
        }
    }

    public void MessageToCaller(Action<SensorData> action)
    {
        if (!Started)
        {
            HubConnection.On(nameof(Message), action);
        }
    }

    public void MessageToGroup(Action<SensorData> action)
    {
        if (!Started)
        {
            HubConnection.On(nameof(MessageToGroup), action);
        }
    }

    public async Task Message(SensorData message)
    {
        await HubConnection.SendAsync(nameof(Message), message);
    }

    public async Task MessageToCaller(SensorData message)
    {
        await HubConnection.SendAsync(nameof(MessageToCaller), message);
    }

    public async Task MessageToGroup(SensorData message)
    {
        await HubConnection.SendAsync(nameof(MessageToGroup), message);
    }
}