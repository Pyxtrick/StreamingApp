namespace StreamingApp.Domain.Entities.Dtos.Twitch;

public class RaidDto(
    string userName,
    int count,
    string game,
    bool isUsed,
    DateTime utcNow)
{
    public int Count { get; set; } = count;

    public string Game { get; set; } = game;

    public string UserName { get; set; } = userName;

    public DateTime utcNow { get; set; } = utcNow;

    public bool IsUsed { get; set; } = isUsed;
}
