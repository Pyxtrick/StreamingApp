using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class SubscriptionDto
{
    public SubscriptionDto(string id, string username, bool isGifftedSub, int gifftedSubCount, TierEnum currentTier, ChatDto? chatMessage)
    {
        Id = id;
        UserName = username;
        IsGifftedSub = isGifftedSub;
        GifftedSubCount = gifftedSubCount;
        CurrentTier = currentTier;
        ChatMessage = chatMessage;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public bool IsGifftedSub { get; set; }

    public int GifftedSubCount { get; set; }

    public TierEnum CurrentTier { get; set; }

    public ChatDto? ChatMessage { get; set; }
}
