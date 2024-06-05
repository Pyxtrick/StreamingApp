using StreamingApp.Domain.Enums;
using TwitchLib.Client.Models;

namespace StreamingApp.Domain.Entities.Dtos;

public class ChatDto
{
    public ChatDto(string id, string userName, string colorHex, string replayMessage, string message, string emoteReplacedMessage, EmoteSet emoteSet, List<KeyValuePair<string, string>> badges, ChatOriginEnum chatOrigin, List<AuthEnum> auth, List<SpecialMessgeEnum> specialMessage, EffectEnum effect, DateTime utcNow)
    {
        Id = id;
        UserName = userName;
        ColorHex = colorHex;
        ReplayMessage = replayMessage;
        Message = message;
        EmoteReplacedMessage = emoteReplacedMessage;
        EmoteSet = emoteSet;
        Badges = badges;
        ChatOrigin = chatOrigin;
        Auth = auth;
        SpecialMessage = specialMessage;
        Effect = effect;
        Date = utcNow;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string ColorHex { get; set; }

    // TODO: implement
    public string ReplayMessage { get; set; }

    public string Message { get; set; }

    public string EmoteReplacedMessage { get; set; }

    public EmoteSet? EmoteSet { get; set; }

    public List<KeyValuePair<string, string>>? Badges { get; set; }

    public ChatOriginEnum ChatOrigin { get; set; }

    public List<AuthEnum> Auth { get; set; }

    public List<SpecialMessgeEnum> SpecialMessage { get; set; }

    public EffectEnum Effect { get; set; }
    public DateTime Date { get; set; }
}
