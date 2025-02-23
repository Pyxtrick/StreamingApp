using StreamingApp.Domain.Enums.Trigger;

namespace StreamingApp.Domain.Entities.Dtos.Twitch;
public class AlertDto
{
    public TriggerCondition TriggerCondition { get; set; }

    public string Name { get; set; }

    public string Message { get; set; }

    public int Volume { get; set; } = 100;

    public byte[]? Image { get; set; }

    public byte[]? Sound { get; set; }

    public byte[]? Video { get; set; }

    public int videoLeght { get; set; }

    public bool IsMute { get; set; }
}
