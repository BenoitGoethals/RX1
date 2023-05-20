using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Rx.Blazor;

public abstract class SignalRClientBase
    : ISignalRClient, IAsyncDisposable
{
  



    protected bool Started { get; private set; }

    protected SignalRClientBase(NavigationManager navigationManager, string hubPath)
    {
        Connection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri(hubPath))
            .WithAutomaticReconnect()
            .Build();
        
    }


    public bool IsConnected =>
        Connection.State == HubConnectionState.Connected;

    protected HubConnection Connection { get; private set; }

    public async ValueTask DisposeAsync()
    {
        await Connection.DisposeAsync();
    }

    public async Task Start()
    {
        if (!Started)
        {
            try
            {
                await Connection.StartAsync();
                Started = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Started = false;
            }

        }
    }
}