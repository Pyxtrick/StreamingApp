using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Internal.User;

public class Ban
{
    public int Id { get; set; }

    public User User { get; set; }

    // Is Currenty Baned
    public bool IsBaned { get; set; }

    // Not add user into any Game Queue
    public bool IsExcludeQueue { get; set; }

    // Exclude to be in any draw's
    public bool ExcludePole { get; set; }

    // Is Chat Banned | messages are not shown on stream
    public bool IsExcludeChat { get; set; }

    public string ExcludeReason { get; set; }

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
