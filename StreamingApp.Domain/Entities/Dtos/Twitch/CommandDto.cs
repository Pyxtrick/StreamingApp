using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos.Twitch;
public class CommandDto(
        string messageId,
        string userId,
        string userName,
        string displayName,
        string message,
        List<AuthEnum> auth,
        DateTime utcNow,
        OriginEnum origin)
        : TwitchBase(messageId, userId, userName, displayName, utcNow)
{
    public string Message { get; set; } = message;

    public List<AuthEnum> Auth { get; set; } = auth;

    public OriginEnum Origin { get; set; } = origin;
}
