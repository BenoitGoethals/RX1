namespace SignalDashBoard;

public interface IClock
{
    Task ShowTime(DateTime currentTime);
}