namespace Rx.Blazor;

public interface ISensorSignalRClient: ISensorHub, ISignalRClient
{
    // Used to allow the clients to take action when the server event occurs
  
    void Message(Action<string, string> action);
    void MessageToCaller(Action<string, string> action);
    void MessageToGroup(Action<string, string> action);
}