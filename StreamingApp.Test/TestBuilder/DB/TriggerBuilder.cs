using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums;
using StreamingApp.Domain.Enums.Trigger;

namespace StreamingApp.Test.TestBuilder.DB;

public static class TriggerBuilder
{
    public static Trigger Create(this UnitOfWorkContext unitOfWork)
    {
        Trigger trigger = new Trigger();
        unitOfWork.Add(trigger);

        return trigger;
    }

    public static Trigger WithDefaults(this Trigger trigger, int id)
    {
        trigger.Id = id;
        trigger.Description = "";
        trigger.TriggerCondition = TriggerCondition.Bits;
        trigger.Ammount = 100;
        trigger.AmmountCloser = false;
        trigger.ExactAmmount = false;
        trigger.Auth = AuthEnum.Undefined;
        trigger.Active = true;
        trigger.ScheduleTime = 10;

        return trigger;
    }

    public static Trigger WithTargets(this Trigger trigger, List<Target> targets)
    {
        trigger.Targets = targets;

        return trigger;
    }

    public static Trigger WithAmmount(this Trigger trigger, int ammount, bool ammountCloser, bool exactAmmount)
    {
        trigger.Ammount = ammount;
        trigger.AmmountCloser = ammountCloser;
        trigger.ExactAmmount = exactAmmount;

        return trigger;
    }

    public static Trigger WithAmmount(this Trigger trigger, bool active)
    {
        trigger.Active = active;

        return trigger;
    }
}
