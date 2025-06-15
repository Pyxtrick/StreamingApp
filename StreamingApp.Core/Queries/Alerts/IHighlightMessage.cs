
namespace StreamingApp.Core.Queries.Alerts;

public interface IHighlightMessage
{
    Task Execute(string messageId);
}