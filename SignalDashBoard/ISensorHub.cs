namespace SignalDashBoard;

public interface ISensorHub
{
    Task SendMessage( string user, string message);
    Task SendMessageToCaller( string user, string message);
    Task SendMessageToGroup( string user, string message);
}