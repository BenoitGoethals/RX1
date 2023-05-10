using rx.core;

namespace Rx.Blazor;

public interface ISensorHub
{
    Task Message(SensorData message);
    Task MessageToCaller(SensorData message);
    Task MessageToGroup(SensorData message);
}