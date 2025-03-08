using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class BannedUserDto
{
    public BannedUserDto(string userId, string messageId, string userName, string lastMessage, string reson, BannedTargetEnum bannedTargetEnum, DateTime date)
    {
        UserId = userId;
        MessageId = messageId;
        UserName = userName;
        LastMessage = lastMessage;
        Reson = reson;
        TargetEnum = bannedTargetEnum;
        Date = date;
    }

    public string UserId { get; set; }

    public string MessageId { get; set; }

    public string UserName { get; set; }

    public string LastMessage { get; set; }

    public string Reson { get; set; }

    public BannedTargetEnum TargetEnum { get; set; }

    public DateTime Date { get; set; }
}
