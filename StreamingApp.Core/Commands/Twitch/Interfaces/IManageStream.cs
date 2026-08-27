namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IManageStream
{
    Task ChangeCategory(bool isCreateStream = false);
    Task StartStream(string youtubeId = "");
    Task EndStream();
    Task CreatePole(string[] splitMessage);
    Task CreatePrediction(string[] splitMessage);
}