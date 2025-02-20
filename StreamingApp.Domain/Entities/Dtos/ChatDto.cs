using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class ChatDto(
    string id,
    string userName,
    string colorHex,
    string? replayMessage,
    string message,
    string emoteReplacedMessage,
    List<EmoteSet>? emoteSet,
    List<KeyValuePair<string, string>> badges,
    ChatOriginEnum chatOrigin,
    ChatDisplayEnum? chatDisplay,
    List<AuthEnum> auth,
    List<SpecialMessgeEnum> specialMessage,
    EffectEnum effect,
    DateTime utcNow)
{
    public string Id { get; set; } = id;

    public string UserName { get; set; } = userName;

    public string ColorHex { get; set; } = colorHex;

    public string? ReplayMessage { get; set; } = replayMessage;

    public string Message { get; set; } = message;

    public string EmoteReplacedMessage { get; set; } = emoteReplacedMessage;

    public List<EmoteSet>? EmoteSet { get; set; } = emoteSet;

    public List<KeyValuePair<string, string>>? Badges { get; set; } = badges;

    public ChatOriginEnum ChatOrigin { get; set; } = chatOrigin;

    public ChatDisplayEnum ChatDisplay { get; set; } = (ChatDisplayEnum)chatDisplay;

    public List<AuthEnum> Auth { get; set; } = auth;

    public List<SpecialMessgeEnum> SpecialMessage { get; set; } = specialMessage;

    public EffectEnum Effect { get; set; } = effect;
    public DateTime Date { get; set; } = utcNow;
}
