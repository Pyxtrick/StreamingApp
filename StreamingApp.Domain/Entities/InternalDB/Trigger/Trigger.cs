using StreamingApp.Domain.Enums;
using StreamingApp.Domain.Enums.Trigger;

namespace StreamingApp.Domain.Entities.InternalDB.Trigger;

public class Trigger : EntityBase
{
    public int Id { get; set; }

    public TriggerCondition TriggerCondition { get; set; }

    public int Ammount { get; set; }

    // false will use lower Value
    public bool AmmountCloser { get; set; }

    public List<Target> Targets { get; set; }

    public bool ExactAmmount { get; set; }

    public AuthEnum Auth { get; set; }

    public bool Active { get; set; }

    public int ScheduleTime { get; set; }
}