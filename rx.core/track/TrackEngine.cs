using rx.core.track.gpx;
using System.Collections.Concurrent;
using CoordinateSharp;

namespace rx.core.track;

public class TrackEngine
{

    public event EventHandler<TrackEngineEventArgs>? TrackEngineEvents;
    private CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    public readonly ConcurrentBag<Track> Tracks = new();
    public bool IsRunning { get; private set; }
    public string? Name { get; set; }

    public TrackEngine(string gpx)
    {
        LoadData(gpx);

    }

    public TrackEngine(Stream? gpx)
    {
        LoadData(gpx);

    }


    private async Task RunProduce()
    {
        if (IsRunning) return;
        IsRunning = true;
        _cancellationToken = _cancellationTokenSource.Token;
        while (!_cancellationToken.IsCancellationRequested)
        {
            foreach (var tr in Tracks)
            {
                var delay = Task.Delay(5000, _cancellationToken);

                TrackEngineEvents?.Invoke(this, new TrackEngineEventArgs() { Track = tr });
                await delay;
            }
        }

    }

    public bool Start()
    {
        if (Tracks.Count > 0)
        {
            Task.Run(async () => { await RunProduce().ConfigureAwait(false); }, _cancellationToken);
        }

        return false;

    }
    public void Stop()
    {
        _cancellationTokenSource.Cancel();

    }

    private void LoadData(Stream? stream)
    {
       GpxReader? reader = null;
        if (stream != null)
        {
            reader = new GpxReader(stream);
        }
       

        if (reader == null) return;
        GetTracks(reader);
    }

    private void LoadData(string gpx)
    {
        Stream? stream = typeof(TrackEngine).Assembly.GetManifestResourceStream(gpx);
        GpxReader? reader = null;
        if (stream != null)
        {
            reader = new GpxReader(File.OpenRead(gpx));
        }

        if(reader == null) return;
        GetTracks(reader);
    }

    private void GetTracks(GpxReader reader)
    {
        using (reader)
        {
            Stream output;

            while (reader.Read())
            {
                switch (reader.ObjectType)
                {
                    case GpxObjectType.Metadata:
                        break;
                    case GpxObjectType.WayPoint:
                        break;
                    case GpxObjectType.Route:
                        foreach (var points in reader.Route.RoutePoints)
                        {
                            Tracks.Add(new Track()
                            {
                                Description = points.Description,
                                Location = new Coordinate(points.Latitude, points.Longitude), Name = reader.Route.Name
                            });
                        }

                        break;
                    case GpxObjectType.Track:
                        break;
                    case GpxObjectType.None:
                        break;
                    case GpxObjectType.Attributes:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}

public class TrackEngineEventArgs : EventArgs
{
    public Track? Track { get; set; }
}