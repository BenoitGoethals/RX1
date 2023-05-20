using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace Rx.Blazor;

public abstract class SignalRClientBase
    : ISignalRClient, IAsyncDisposable
{
  



    protected bool Started { get; private set; }

    protected SignalRClientBase(NavigationManager navigationManager, string hubPath) =>
        HubConnection = new HubConnectionBuilder()
            .WithUrl(navigationManager.ToAbsoluteUri(hubPath))
            .WithAutomaticReconnect()
            .Build();

    public bool IsConnected =>
        HubConnection.State == HubConnectionState.Connected;

    protected HubConnection HubConnection { get; private set; }

    public async ValueTask DisposeAsync()
    {
        await HubConnection.DisposeAsync();
    }

    public async Task Start()
    {
        if (!Started)
        {
            try
            {
                await HubConnection.StartAsync();
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