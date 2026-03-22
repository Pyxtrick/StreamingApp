using StreamingApp.DB;
using StreamingApp.Domain.Entities.InternalDB.Trigger;

namespace StreamingApp.Test.TestBuilder.DB;

public static class AlertBuilder
{
    public static Alert Create(this UnitOfWorkContext unitOfWork)
    {
        Alert alert = new Alert();
        unitOfWork.Add(alert);

        return alert;
    }

    public static Alert WithDefaults(this Alert alert, int id)
    {
        alert.Id = id;
        alert.Name = "";
        alert.Volume = 100;
        alert.Image = null;
        alert.Sound = null;
        alert.Video = null;
        alert.Html = null;
        alert.videoLeght = 10;
        alert.TimesUsed = 0;
        alert.IsActive = true;
        alert.IsMute = false;
        alert.UpdatedBy = "";
        alert.UpdatedAt = DateTime.UtcNow;

        return alert;
    }

    public static Alert WithDefaults(this Alert alert, List<TargetData> targetData)
    {
        alert.TargetData = targetData;

        return alert;
    }
}