namespace StreamingApp.Core.Utility.Scheduler;

public class ActivitySchedulerOptions
{
    public const string ActivityScheduler = nameof(ActivityScheduler);

    public double ActivityRefreshInDays { get; set; }

    public int WeeksToRetain { get; set; }
}
