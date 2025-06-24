using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos.Twitch;

public class SubDto(string messageId,
    string userId,
    string userName,
    string displayName,
    string channel,
    OriginEnum origin,
    bool isGifftedSub,
    int gifftedSubCount,
    int subLenght,
    TierEnum currentTier,
    MessageDto? chatMessage,
    bool isUsed,
    DateTime utcNow)
    : TwitchBase(messageId, userId, userName, displayName, utcNow)
{
    public string Channel { get; set; } = channel;

    public OriginEnum Origin { get; set; } = origin;

    public bool IsGifftedSub { get; set; } = isGifftedSub;

    public int GifftedSubCount { get; set; } = gifftedSubCount;

    public int SubLenght { get; set; } = subLenght;

    public TierEnum CurrentTier { get; set; } = currentTier;

    public MessageDto? ChatMessage { get; set; } = chatMessage;

    public bool IsUsed { get; set; } = isUsed;
}
