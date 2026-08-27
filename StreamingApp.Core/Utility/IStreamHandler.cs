namespace StreamingApp.Core.Utility;

public interface IStreamHandler
{
    Task<bool> Start(string youtubeId = "");
    Task<bool> Stop();
}