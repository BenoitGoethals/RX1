namespace Rx.Blazor;

public interface ISensorHub
{
    Task Message( string user, string message);
    Task MessageToCaller( string user, string message);
    Task MessageToGroup( string user, string message);
}