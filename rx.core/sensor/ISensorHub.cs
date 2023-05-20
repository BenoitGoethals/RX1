namespace rx.core.sensor;

public interface ISensorHub
{
    Task Message(SensorData message);
    Task MessageToCaller(SensorData message);
    Task MessageToGroup(SensorData message);

    Task StartStop(string server);

}