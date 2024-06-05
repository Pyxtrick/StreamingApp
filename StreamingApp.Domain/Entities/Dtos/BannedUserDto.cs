namespace StreamingApp.Domain.Entities.Dtos;

public class BannedUserDto
{
    public BannedUserDto(string userName, string lastMessage, DateTime date)
    {
        UserName = userName;
        LastMessage = lastMessage;
        Date = date;
    }

    public int Id { get; set; }

    public string UserName { get; set; }

    public string LastMessage { get; set; }

    public DateTime Date { get; set; }
}
