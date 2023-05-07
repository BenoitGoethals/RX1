using System.Collections.Concurrent;
using System.Reflection;

namespace rx.core;

public class TrackEngine
{
  
    public event EventHandler<TrackEngineEventArgs>? TrackEngineEvents;
    private CancellationToken _cancellationToken;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly ConcurrentBag<Track> _tracks = new();
    public bool IsRunning { get; private set; }
    public TrackEngine()
    {
        LoadData();
      
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
                var delay = Task.Delay(500, _cancellationToken);

                TrackEngineEvents?.Invoke(this, new TrackEngineEventArgs() { Track = tr });
                await delay;
            }


        }
       
    }

    public void  Start()
    {
         Task.Run(async () => { await RunProduce().ConfigureAwait(false); }, _cancellationToken);
     
    }
    public void Stop()
    {
        _cancellationTokenSource.Cancel();

    }
    private void LoadData()
    {
     var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("rx.core.geo.csv");

        if (stream == null) return;
        using var reader = new StreamReader(stream);
        reader.ReadLine();
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            _tracks.Add(Track.FromCsv(line));
        }
    }

}

public class TrackEngineEventArgs : EventArgs
    {
        public Track Track { get; set; }
    }