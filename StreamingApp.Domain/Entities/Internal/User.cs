using System.ComponentModel.DataAnnotations.Schema;

namespace StreamingApp.Domain.Entities.Internal;

public class User
{
    public int Id { get; set; }

    // can be used to set specific text what the person is or other tings
    public string? UserText { get; set; }

    public int TwitchDetailId { get; set; }
    public UserDetail? TwitchDetail { get; set; }

    //public int TwitterDetailId { get; set; }
    //public UserDetail TwitterDetail { get; set; }

    //public int YoutubeDetailId { get; set; }
    //public UserDetail YoutubeDetail { get; set; }

    //public int DiscordDetailId { get; set; }
    //public UserDetail DiscordDetail { get; set; }

    public int StatusId { get; set; }
    public Status? Status { get; set; }

    public int TwitchAchievementsId { get; set; }
    public Achievements? TwitchAchievements { get; set; }

    //public int YoutubeAchievementsId { get; set; }
    //public Achievements YoutubeAchievements { get; set; }

    public int BanId { get; set; }
    public Ban? Ban { get; set; }
}
