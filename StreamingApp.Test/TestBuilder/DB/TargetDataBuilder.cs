using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Trigger;

namespace StreamingApp.Test.TestBuilder.DB;

public static class TargetDataBuilder
{
    public static TargetData Create(this UnitOfWorkContext unitOfWork)
    {
        TargetData targetData = new TargetData();
        unitOfWork.Add(targetData);

        return targetData;
    }

    public static TargetData WithDefaults(this TargetData targetData, int id)
    {
        targetData.Id = id;
        targetData.Text = "Test";
        targetData.Duration = 1;
        targetData.Size = 1;

        return targetData;
    }

    public static TargetData WithTarget(this TargetData targetData, Target target)
    {
        targetData.Target = target;
        return targetData;
    }

    public static TargetData WithAlert(this TargetData targetData, Alert alert)
    {
        targetData.Alert = alert;

        return targetData;
    }
}
