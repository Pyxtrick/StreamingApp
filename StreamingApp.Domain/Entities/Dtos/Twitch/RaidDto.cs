namespace StreamingApp.Domain.Entities.Dtos.Twitch;

public class RaidDto(
    string userName,
    int count,
    string game,
    bool isUsed,
    DateTime utcNow)
{
    public string UserName { get; set; } = userName;

    public int Count { get; set; } = count;

    public string Game { get; set; } = game;

    public bool IsUsed { get; set; } = isUsed;

    public DateTime utcNow { get; set; } = utcNow;
}
