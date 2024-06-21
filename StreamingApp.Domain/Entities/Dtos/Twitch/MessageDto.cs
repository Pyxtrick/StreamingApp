using StreamingApp.Domain.Enums;
using TwitchLib.Client.Models;

namespace StreamingApp.Domain.Entities.Dtos.Twitch;
public class MessageDto(
    string messageId,
    bool isCommand,
    string userId,
    string userName,
    string colorHex,
    string replayMessage,
    string message,
    string emoteReplacedMessage,
    string pointRediam,
    int bits,
    EmoteSet emoteSet,
    List<KeyValuePair<string, string>> badges,
    ChatOriginEnum chatOrigin,
    List<AuthEnum> auth,
    List<SpecialMessgeEnum> specialMessage,
    EffectEnum effect,
    bool isSub,
    int subCount,
    DateTime utcNow)
    : TwitchBase(messageId, userId, userName, utcNow)
{
    public bool IsCommand { get; set; } = isCommand;

    public string ColorHex { get; set; } = colorHex;

    public string ReplayMessage { get; set; } = replayMessage;

    public string Message { get; set; } = message;

    public string EmoteReplacedMessage { get; set; } = emoteReplacedMessage;

    public string PointRediam { get; set; } = pointRediam;

    public int Bits { get; set; } = bits;

    public EmoteSet? EmoteSet { get; set; } = emoteSet;

    // Prediction / Sub / mod / vip / staff / Verified / bit / gif / hypetrain / prime / turbo / events
    public List<KeyValuePair<string, string>>? Badges { get; set; } = badges;

    public ChatOriginEnum ChatOrigin { get; set; } = chatOrigin;

    public List<AuthEnum> Auth { get; set; } = auth;

    public List<SpecialMessgeEnum> SpecialMessage { get; set; } = specialMessage;

    public EffectEnum Effect { get; set; } = effect;

    public bool IsSub { get; set; } = isSub;

    public int SubCount { get; set; } = subCount;
}
