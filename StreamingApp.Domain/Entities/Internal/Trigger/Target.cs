using StreamingApp.Domain.Enums.Trigger;

namespace StreamingApp.Domain.Entities.Internal.Trigger;

public class Target
{
    public int Id { get; set; }

    public TargetCondition TargetCondition { get; set; }

    public int TriggerId { get; set; }
    public Trigger Trigger { get; set; }

    public string TargetDataId { get; set; }
    public TargetData TargetData { get; set; }

    public bool IsSameTime { get; set; }

    public int Chance { get; set; }
}
