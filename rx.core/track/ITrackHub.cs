namespace rx.core.track;

public interface ITrackHub
{

    Task SendTrack(Track track);
    Task SendTrackToCaller(Track track);
    Task SendTrackToGroup(Track track);

    Task StartStopTracker(string server);
}