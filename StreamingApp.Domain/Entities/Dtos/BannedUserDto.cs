using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Dtos;

public class BannedUserDto
{
    public BannedUserDto(string id, string userName, string lastMessage, string reson, BannedTargetEnum bannedTargetEnum, DateTime date)
    {
        Id = id;
        UserName = userName;
        LastMessage = lastMessage;
        Reson = reson;
        TargetEnum = bannedTargetEnum;
        Date = date;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string LastMessage { get; set; }

    public string Reson { get; set; }

    public BannedTargetEnum TargetEnum { get; set; }

    public DateTime Date { get; set; }
}
