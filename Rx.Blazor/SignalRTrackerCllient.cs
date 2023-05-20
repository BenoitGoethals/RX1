using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using rx.core.sensor;
using rx.core.track;

namespace Rx.Blazor;

public class SignalRTrackerClient : SignalRClientBase, IBackendSignalRTrackerClient
{
    public SignalRTrackerClient(NavigationManager navigationManager)
        : base(navigationManager, "https://localhost:7109/TrackHub")
    {
    }


    public void SendTrack(Action<TrackDto> action)
    {

        if (!Started)
        {
            Connection.On("SendTrack", action);
        }
    }

    public void SendTrackToCaller(Action<TrackDto> action)
    {

        if (!Started)
        {
            Connection.On("SendTrackToCaller", action);
        }
    }

    public void SendTrackToGroup(Action<TrackDto> action)
    {
        if (!Started)
        {
            Connection.On(nameof(SendTrackToGroup), action);
        }
    }

    public void StartStopTracker(string server)
    {
        Connection.SendAsync("StartStopTracker", server);
    }

  

    public async Task SendTrack(Track message)
    {
        await Connection.SendAsync(nameof(SendTrack), message);
    }

    public async Task SendTrackToCaller(Track message)
    {
        await Connection.SendAsync(nameof(SendTrackToCaller), message);
    }

    public async Task SendTrackToGroup(Track message)
    {
        await Connection.SendAsync(nameof(SendTrackToGroup), message);
    }
}