namespace StreamingApp.Domain.Entities.Dtos.Twitch;
public class TwitchBase(
    string messageId,
    string userId,
    string userName,
    DateTime utcNow)
{
    public string MessageId { get; set; } = messageId;

    public string UserId { get; set; } = userId;

    public string UserName { get; set; } = userName;

    public DateTime Date { get; set; } = utcNow;
}
