using rx.core.track;

namespace Rx.Blazor;

public interface IBackendSignalRTrackerClient : ISignalRClient
{

    void SendTrack(Action<TrackDto> action);
    void SendTrackToCaller(Action<TrackDto> action);
    void SendTrackToGroup(Action<TrackDto> action);
    void StartStopTracker(string server);
}