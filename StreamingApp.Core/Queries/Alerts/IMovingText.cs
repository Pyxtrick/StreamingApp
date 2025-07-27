
namespace StreamingApp.Core.Queries.Alerts;

public interface IMovingText
{
    Task ExecuteAlert(int adLength, string text);
}