using Microsoft.AspNetCore.SignalR;
using rx.core.sensor;
using rx.core.track;
using System.Text.RegularExpressions;

namespace SignalDashBoard;

public class TrackHub : Hub<ITrackHub>
{
    private readonly IServiceProvider _service;

    public TrackHub(IServiceProvider service)
    {
        this._service = service;
    }

    public async Task SendTrack(TrackDto track)
        => await Clients.All.SendTrack(track);

    public async Task SendTrackToCaller(TrackDto track)
        => await Clients.Caller.SendTrack(track);

    public async Task SendTrackToGroup(TrackDto track)
        => await Clients.All.SendTrackToGroup(track);

    public async Task StartStop(string server)
    {

        var worker = _service.GetServices<IHostedService>().OfType<TrackWorker>().Single();
        if (worker.Engines != null)
        {
            var obsServer = worker.Engines.First(obs => obs.Name == server);
            if (obsServer.IsRunning)
            {
                obsServer.Stop();
            }
            else
            {
                obsServer.Start();
            }
        }
    }
}