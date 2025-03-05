using StreamingApp.Domain.Enums.Trigger;

namespace StreamingApp.Domain.Entities.Dtos.Twitch;
public class AlertDto
{
    // Volume at with the audio is played at 100 is default
    public int Volume { get; set; } = 100;

    public byte[]? Image { get; set; }

    public byte[]? Sound { get; set; }

    public byte[]? Video { get; set; }

    public string Html { get; set; }

    public int videoLeght { get; set; }

    public bool IsMute { get; set; }

    public int Duration { get; set; }

    public bool IsSameTime { get; set; }
}
