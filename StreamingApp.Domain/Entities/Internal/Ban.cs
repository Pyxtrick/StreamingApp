using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Internal;

public class Ban
{
    public int Id { get; set; }

    // Is Currenty Baned
    public bool IsBaned { get; set; }

    // Is Chat Banned | messages are not shown on stream
    public bool IsMessageBaned { get; set; }

    // Has X timeout time
    public int TimeOutAmount { get; set; }

    // Has X messages deleted
    public int MessagesDeletedAmount { get; set; }

    // has  Been banned for x Times
    public int BanedAmount { get; set; }

    // Has been banned at Date
    public DateTime BanedDate { get; set; }

    // Message for the message
    [Required]
    public string LastMessage { get; set; }
}
