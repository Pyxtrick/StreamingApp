namespace StreamingApp.Domain.Entities.InternalDB.User;

public class User
{
    public int Id { get; set; }

    // can be used to set specific text what the person is or other tings
    public string? UserText { get; set; }
    
    public List<UserDetail> Details { get; set; }

    //public int DiscordDetailId { get; set; }
    //public UserDetail DiscordDetail { get; set; }

    public Status? Status { get; set; }

    public List<Achievements> Achievements { get; set; }
    
    public int BanId { get; set; }
    public Ban? Ban { get; set; }
}
