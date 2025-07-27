namespace StreamingApp.Core.Commands.Twitch.Interfaces;

public interface IPointRedeam
{
    Task Execute(string userName, string userId, string rewardid, string rewardName, string rewardPrompt);
}