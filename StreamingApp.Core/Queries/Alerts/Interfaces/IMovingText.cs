namespace StreamingApp.Core.Queries.Alerts.Interfaces;

public interface IMovingText
{
    Task ExecuteAlert(int adLength, string text);
}