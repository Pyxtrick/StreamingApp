using System.ComponentModel.DataAnnotations;

namespace StreamingApp.Domain.Entities.Internal.Trigger;

public class Emote
{
    public int Id { get; set; }

    public List<TargetData> TargetData { get; set; }

    [Required]
    public string Name { get; set; }

    // Volume at with the audio is played at 100 is default
    public int Volume { get; set; } = 100;

    public byte[]? Image { get; set; }

    public byte[]? Sound { get; set; }

    public byte[]? Video { get; set; }

    public int videoLeght { get; set; }

    public int TimesUsed { get; set; }

    public bool IsActive { get; set; }

    public bool IsMute { get; set; }

    public string UpdatedBy { get; set; }

    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.Now;
}
