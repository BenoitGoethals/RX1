namespace Rx.Blazor;

public interface ISignalRClient
{
    bool IsConnected { get; }
    Task Start();
}