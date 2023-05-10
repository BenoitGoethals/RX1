using rx.core;

namespace Rx.Blazor;

public interface ISensorSignalRClient: ISensorHub, ISignalRClient
{
    // Used to allow the clients to take action when the server event occurs
  
    void Message(Action<SensorData> action);
    void MessageToCaller(Action<SensorData> action);
    void MessageToGroup(Action<SensorData> action);
    void StartStop(string server);



}