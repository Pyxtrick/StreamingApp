using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos.Twitch;

/// <summary>
/// Message Alert
/// </summary>
public class MessageAlertDto(
    string messageId,
    string channel,
    string userId,
    string userName,
    string displayName,
    string colorHex,
    string message,
    string emoteReplacedMessage,
    string? pointRediam,
    int bits,
    List<EmoteSet> emotes,
    List<KeyValuePair<string, string>> badges,
    ChatOriginEnum chatOrigin,
    AlertTypeEnum alertType,
    List<AuthEnum> auth,
    bool isSub,
    bool isUsed,
    DateTime utcNow)
    : TwitchBase(messageId, userId, userName, displayName, utcNow)
{
    public string Channel { get; set; } = channel;

    public string ColorHex { get; set; } = colorHex;

    public string Message { get; set; } = message;

    public string EmoteReplacedMessage { get; set; } = emoteReplacedMessage;

    public string? PointRediam { get; set; } = pointRediam;

    public int Bits { get; set; } = bits;

    public List<EmoteSet> Emotes { get; set; } = emotes;

    // Prediction / Sub / mod / vip / staff / Verified / bit / gif / hypetrain / prime / turbo / events
    public List<KeyValuePair<string, string>>? Badges { get; set; } = badges;

    public ChatOriginEnum ChatOrigin { get; set; } = chatOrigin;

    public AlertTypeEnum AlertType { get; set; } = alertType;

    public List<AuthEnum> Auth { get; set; } = auth;

    public bool IsSub { get; set; } = isSub;

    public bool IsUsed { get; set; } = isUsed;
}
