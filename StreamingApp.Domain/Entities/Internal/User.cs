using System.ComponentModel.DataAnnotations.Schema;

namespace StreamingApp.Domain.Entities.Internal;

public class User
{
    public int Id { get; set; }

    // can be used to set specific text what the person is or other tings
    public string? UserText { get; set; }

    public int TwitchDetailId { get; set; }
    [NotMapped]
    public UserDetail? TwitchDetail { get; set; }

    //public int TwitterDetailId { get; set; }
    //[NotMapped]
    //public UserDetail TwitterDetail { get; set; }

    //public int YoutubeDetailId { get; set; }
    //[NotMapped]
    //public UserDetail YoutubeDetail { get; set; }

    //public int DiscordDetailId { get; set; }
    //[NotMapped]
    //public UserDetail DiscordDetail { get; set; }

    public int StatusId { get; set; }
    [NotMapped]
    public Status? Status { get; set; }

    public int TwitchAchievementsId { get; set; }
    [NotMapped]
    public Achievements? TwitchAchievements { get; set; }

    //public int YoutubeAchievementsId { get; set; }
    //[NotMapped]
    //public Achievements YoutubeAchievements { get; set; }

    public int BanId { get; set; }
    [NotMapped]
    public Ban? Ban { get; set; }
}
