using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Trigger;
using StreamingApp.Domain.Enums.Trigger;

namespace StreamingApp.Test.TestBuilder.DB;

public static class TargetBuilder
{
    public static Target Create(this UnitOfWorkContext unitOfWork)
    {
        Target target = new Target();
        unitOfWork.Add(target);

        return target;
    }

    public static Target WithDefaults(this Target target, int id, TargetCondition targetCondition)
    {
        target.Id = id;
        target.TargetCondition = targetCondition;
        target.IsSameTime = false;
        target.Chance = 100;

        return target;
    }

    public static Target WitTrigger(this Target target, Trigger trigger)
    {
        target.TriggerId = trigger.Id;
        target.Trigger = trigger;

        return target;
    }

    public static Target WithTargetData(this Target target, TargetData targetData)
    {
        target.TargetDataId = targetData.Id;
        target.TargetData = targetData;

        return target;
    }

    public static Target WithCommandAndResponse(this Target target, CommandAndResponse commandAndResponse)
    {
        target.CommandAndResponseId = commandAndResponse.Id;
        target.CommandAndResponse = commandAndResponse;

        return target;
    }
}
