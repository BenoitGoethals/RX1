using rx.core.sensor;
using rx.core.track;

namespace Rx.Blazor;

public interface IBackendSignalRClient:  ISignalRClient
{
    // Used to allow the clients to take action when the server event occurs
  
    void Message(Action<SensorData> action);
    void MessageToCaller(Action<SensorData> action);
    void MessageToGroup(Action<SensorData> action);
    void StartStopSensor(string server);





}