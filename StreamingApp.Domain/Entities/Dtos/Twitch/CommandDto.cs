using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos.Twitch;
public class CommandDto(
        string messageId,
        string userId,
        string userName,
        string message,
        List<AuthEnum> auth,
        DateTime utcNow,
        ChatOriginEnum origin)
        : TwitchBase(messageId, userId, userName, utcNow)
{
    public string Message { get; set; } = message;

    public List<AuthEnum> Auth { get; set; } = auth;

    public ChatOriginEnum Origin { get; set; } = origin;
}
