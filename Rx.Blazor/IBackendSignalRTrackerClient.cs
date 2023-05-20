using rx.core.track;

namespace Rx.Blazor;

public interface IBackendSignalRTrackerClient : ISignalRClient
{

    void SendTrack(Action<Track> action);
    void SendTrackToCaller(Action<Track> action);
    void SendTrackToGroup(Action<Track> action);
    void StartStopTracker(string server);
}