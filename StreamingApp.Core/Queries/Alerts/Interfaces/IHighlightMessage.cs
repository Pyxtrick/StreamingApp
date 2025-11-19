namespace StreamingApp.Core.Queries.Alerts.Interfaces;

public interface IHighlightMessage
{
    Task Execute(string messageId);
}