using StreamingApp.Domain.Enums;

namespace StreamingApp.Domain.Entities.Internal;

public class SpecialWordDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Comment { get; set; }

    public SpecialWordEnum Type { get; set; }

    public int TimesUsed { get; set; }
}
