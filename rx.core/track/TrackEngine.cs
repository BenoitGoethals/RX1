using CoordinateSharp.Debuggers;
using rx.core.track.gpx;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Resources;
using CoordinateSharp;

namespace rx.core.track;

public class TrackEngine
{

    public event EventHandler<TrackEngineEventArgs>? TrackEngineEvents;
    private CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly ConcurrentBag<Track> _tracks = new();
    public bool IsRunning { get; private set; }
    public string? Name { get; set; }

    public TrackEngine(string gpx)
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

            foreach (var tr in _tracks)
            {
                var delay = Task.Delay(5000, _cancellationToken);

                TrackEngineEvents?.Invoke(this, new TrackEngineEventArgs() { Track = tr });
                await delay;
            }


        }

    }

    public bool Start()
    {
        if (_tracks.Count > 0)
        {
            Task.Run(async () => { await RunProduce().ConfigureAwait(false); }, _cancellationToken);
        }

        return false;

    }
    public void Stop()
    {
        _cancellationTokenSource.Cancel();

    }
    private void LoadData(string gpx)
    {
        Stream? stream = typeof(TrackEngine).Assembly.GetManifestResourceStream("rx.core." + gpx);

        GpxReader? reader = null;
        if (stream != null)
        {
            reader = new GpxReader(stream);
        }
        else if (File.Exists(gpx) && stream == null)
        {
            reader = new GpxReader(File.OpenRead(gpx));
        }

        if(reader == null) return;
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
                            _tracks.Add(new Track() { Description = points.Description, Location = new Coordinate(points.Latitude, points.Longitude), Name = reader.Route.Name });
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