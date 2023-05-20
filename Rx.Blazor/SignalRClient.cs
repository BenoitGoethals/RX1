using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using rx.core.sensor;
using rx.core.track;

namespace Rx.Blazor;

public class SignalRClient : SignalRClientBase, IBackendSignalRClient
{
    public SignalRClient(NavigationManager navigationManager)
        : base(navigationManager, "https://localhost:7109/SensorHub")
    {
    }

    public void Message(Action<SensorData> action)
    {
        if (!Started)
        {
            Connection.On("Message", action);
        }
    }

    public void MessageToCaller(Action<SensorData> action)
    {
        if (!Started)
        {
            Connection.On(nameof(MessageToCaller), action);
        }
    }

    public void MessageToGroup(Action<SensorData> action)
    {
        if (!Started)
        {
            Connection.On(nameof(MessageToGroup), action);
        }
    }

    public void StartStopSensor(string server)
    {
        Connection.SendAsync("StartStop", server);
    }




    public async Task Message(SensorData message)
    {
        await Connection.SendAsync(nameof(Message), message);
    }

    public async Task MessageToCaller(SensorData message)
    {
        await Connection.SendAsync(nameof(MessageToCaller), message);
    }

    public async Task MessageToGroup(SensorData message)
    {
        await Connection.SendAsync(nameof(MessageToGroup), message);
    }


}