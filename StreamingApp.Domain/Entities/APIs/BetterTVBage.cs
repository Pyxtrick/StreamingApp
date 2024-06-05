using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.APIs;

public class BetterTVBage
{
    public string? Description { get; set; }

    public string? Svg { get; set; }

    public BageTypeEnum Type { get; set; }
}
