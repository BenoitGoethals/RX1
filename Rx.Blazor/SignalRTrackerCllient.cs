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


    public void SendTrack(Action<Track> action)
    {

        if (!Started)
        {
            HubConnection.On("SendTrack", action);
        }
    }

    public void SendTrackToCaller(Action<Track> action)
    {

        if (!Started)
        {
            HubConnection.On("SendTrackToCaller", action);
        }
    }

    public void SendTrackToGroup(Action<Track> action)
    {
        if (!Started)
        {
            HubConnection.On(nameof(SendTrackToGroup), action);
        }
    }

    public void StartStopTracker(string server)
    {
        HubConnection.SendAsync("StartStopTracker", server);
    }

  

    public async Task SendTrack(Track message)
    {
        await HubConnection.SendAsync(nameof(SendTrack), message);
    }

    public async Task SendTrackToCaller(Track message)
    {
        await HubConnection.SendAsync(nameof(SendTrackToCaller), message);
    }

    public async Task SendTrackToGroup(Track message)
    {
        await HubConnection.SendAsync(nameof(SendTrackToGroup), message);
    }
}