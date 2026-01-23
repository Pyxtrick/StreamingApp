using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos.Twitch;

/// <summary>
/// Message with all Inortant information comming from Twitch / (Youtube)
/// new essageDto(isCommand, channel, colorHex replayMessage, message, emoteReplacedMessage, emotes, badges, origin, auth, specialMessage, effect, isSub, subCount, isUsed, messageId, userId, userName, displayName, utcNow)
/// </summary>
public class MessageDto(
    bool isCommand,
    string channel,
    string colorHex,
    string? replayMessage,
    string message,
    string emoteReplacedMessage,
    List<EmoteSetDto> emotes,
    List<KeyValuePair<string, string>> badges,
    OriginEnum origin,
    List<AuthEnum> auth,
    List<SpecialMessgeEnum> specialMessage,
    EffectEnum effect,
    bool isSub,
    int subCount,
    bool isUsed,
    string messageId,
    string userId,
    string userName,
    string displayName,
    DateTime utcNow)
    : TwitchBase(messageId, userId, userName, displayName, utcNow)
{
    public bool IsCommand { get; set; } = isCommand;

    public string Channel { get; set; } = channel;

    public string ColorHex { get; set; } = colorHex;

    public string? ReplayMessage { get; set; } = replayMessage;

    public string Message { get; set; } = message;

    public string EmoteReplacedMessage { get; set; } = emoteReplacedMessage;

    public List<EmoteSetDto> Emotes { get; set; } = emotes;

    // Prediction / Sub / mod / vip / staff / Verified / bit / gif / hypetrain / prime / turbo / events
    public List<KeyValuePair<string, string>>? Badges { get; set; } = badges;

    public OriginEnum Origin { get; set; } = origin;

    public List<AuthEnum> Auth { get; set; } = auth;

    public List<SpecialMessgeEnum> SpecialMessage { get; set; } = specialMessage;

    public EffectEnum Effect { get; set; } = effect;

    public bool IsSub { get; set; } = isSub;

    public int SubCount { get; set; } = subCount;

    public bool IsUsed { get; set; } = isUsed;
}
