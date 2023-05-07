namespace Rx.Blazor;

public interface IClock
{
    Task ShowTime(DateTime currentTime);
}