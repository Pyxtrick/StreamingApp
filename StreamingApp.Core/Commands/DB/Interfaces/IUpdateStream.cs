namespace StreamingApp.Core.Commands.DB.Interfaces;

public interface IUpdateStream
{
    Task ChangeCategory(string categoryName);
    Task StartOrEndStream(string streamTitle, string categoryName);
}