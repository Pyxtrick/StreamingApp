using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Internal;

public class Status
{
    public int Id { get; set; }

    public int TwitchSubId { get; set; }

    public User User { get; set; }

    public int MyProperty { get; set; }
    public Sub? TwitchSub { get; set; }

    //public int YoutubeSubId { get; set; }
    //public Sub YoutubeSub { get; set; }

    // Date of first Message
    public DateTime FirstChatDate { get; set; }

    // Fallowing since
    public DateTime FallowDate { get; set; }

    // Is Currenty VIP | defined externaly
    public bool IsVIP { get; set; }

    // Is Currenty Verified / Streamer | defined externaly
    public bool IsVerified { get; set; }

    // Is Currenty a moderator on Twitch or Yoututbe
    public bool IsMod { get; set; }

    // What is that person default: Viewer | defined internal
    public UserTypeEnum UserType { get; set; }

    // time zone for other streamers | defined internal
    public string TimeZone { get; set; }
}
