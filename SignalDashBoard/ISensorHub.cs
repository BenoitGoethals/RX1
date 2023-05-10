using rx.core;

namespace SignalDashBoard;

public interface ISensorHub
{
    Task SendMessage(SensorData sensorData);
    Task SendMessageToCaller(SensorData sensorData);
    Task SendMessageToGroup(SensorData sensorData);

    Task StartStop(string server);

    
}