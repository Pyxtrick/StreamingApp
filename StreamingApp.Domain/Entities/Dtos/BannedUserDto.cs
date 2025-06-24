using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class BannedUserDto(string userId, string messageId, string userName, string lastMessage, string reson, BannedTargetEnum targetEnum, bool isUsed, OriginEnum origin, DateTime date)
    {

    public string UserId { get; set; } = userId;

    public string MessageId { get; set; } = messageId;

    public string UserName { get; set; } = userName;

    public string LastMessage { get; set; } = lastMessage;

    public string Reson { get; set; } = reson;

    public BannedTargetEnum TargetEnum { get; set; } = targetEnum;

    public bool IsUsed { get; set; } = isUsed;

    public OriginEnum Origin { get; set; } = origin;

    public DateTime Date { get; set; } = DateTime.UtcNow;
}
