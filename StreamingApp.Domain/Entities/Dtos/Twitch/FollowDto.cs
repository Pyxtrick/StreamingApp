using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos.Twitch;

public class FollowDto(
    string messageId,
    string userId,
    string userName,
    string displayName,
    bool isGifftedSub,
    int gifftedSubCount,
    TierEnum currentTier,
    MessageDto? chatMessage,
    DateTime utcNow)
    : TwitchBase(messageId, userId, userName, displayName, utcNow)
{
    public bool IsGifftedSub { get; set; } = isGifftedSub;

    public int GifftedSubCount { get; set; } = gifftedSubCount;

    public TierEnum CurrentTier { get; set; } = currentTier;

    public MessageDto? ChatMessage { get; set; } = chatMessage;
}
