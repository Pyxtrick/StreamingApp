using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class SubscriptionDto
{
    public SubscriptionDto(string id, string username, string? recipientUserName, bool isGifftedSub, int gifftedSubCount, TierEnum currentTier, MessageDto? chatMessage)
    {
        Id = id;
        UserName = username;
        RecipientUserName = recipientUserName;
        IsGifftedSub = isGifftedSub;
        GifftedSubCount = gifftedSubCount;
        CurrentTier = currentTier;
        ChatMessage = chatMessage;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string? RecipientUserName { get; set; }

    public bool IsGifftedSub { get; set; }

    public int GifftedSubCount { get; set; }

    public TierEnum CurrentTier { get; set; }

    public MessageDto? ChatMessage { get; set; }
}
