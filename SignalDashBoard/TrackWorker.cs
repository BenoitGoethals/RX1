using rx.core.sensor;
using System.Collections.Concurrent;
using rx.core.track;
using Microsoft.AspNetCore.Routing;
using System.Reactive.Linq;
using Microsoft.AspNetCore.SignalR;

namespace SignalDashBoard;

public class TrackWorker : BackgroundService, IDisposable
{
    private readonly ILogger<Worker> _logger;
    private readonly IHubContext<TrackHub, ITrackHub> _hubContext;
    public ConcurrentBag<TrackEngine>? Engines { get; private set; } = new();

    public TrackWorker(ILogger<Worker> logger, IHubContext<TrackHub, ITrackHub> hubContext)
    {
        _logger = logger;
        Engines?.Add(new TrackEngine("route.gpx"));
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (Engines != null)
            foreach (var engine in Engines)
            {
                Observable.FromEventPattern<TrackEngineEventArgs>(
                        h => engine.TrackEngineEvents += h,
                        h => engine.TrackEngineEvents -= h)
                    .Select(x => x.EventArgs.Track).Subscribe(t =>
                    {
                        if (t == null) return;
                        if (t.Location != null)
                            _hubContext.Clients.All.SendTrack(new TrackDto()
                            {
                                Description = t.Description,
                                latitude = t.Location.Latitude.DecimalDegree,
                                Longitude = t.Location.Longitude.DecimalDegree,
                                Name = t.Name,
                                SymbolCode = t.SymbolCode
                            });
                    });

                engine.Start();
            }

        while (!stoppingToken.IsCancellationRequested)
        {

            await Task.Delay(1000, stoppingToken);
        }
    }
}