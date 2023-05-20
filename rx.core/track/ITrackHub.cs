namespace rx.core.track;

public interface ITrackHub
{

    Task SendTrack(TrackDto track);
    Task SendTrackToCaller(TrackDto track);
    Task SendTrackToGroup(TrackDto track);

    Task StartStopTracker(string server);
}