namespace StreamingApp.Domain.Entities.Dtos.Twitch;

public class RaidDto(
    string messageId,
    string userId,
    string userName,
    int count,
    string game,
    DateTime utcNow)
    : TwitchBase(messageId, userId, userName, utcNow)
{
    public int Count { get; set; } = count;

    public string Game { get; set; } = game;
}
