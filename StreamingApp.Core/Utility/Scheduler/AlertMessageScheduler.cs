using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Internal.Trigger;

namespace StreamingApp.Core.Utility.Scheduler;

public class AlertMessageScheduler : BackgroundService
{
    private readonly ILogger<AlertScheduler> _logger;

    private readonly IServiceProvider _serviceProvider;

    private readonly IConfiguration _configuration;

    private Timer _timer;

    private int timer = 0;

    public AlertMessageScheduler(IServiceProvider serviceProvider, ILogger<AlertScheduler> logger, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        _configuration = configuration;

        timer = Int32.Parse(_configuration["Scheduler:Activity"]);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return base.StopAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        List<Trigger> triggers = new();

        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            triggers = scope.ServiceProvider.GetRequiredService<UnitOfWorkContext>().Trigger.Include("Targets.CommandAndResponse").Where(t => t.TriggerCondition == Domain.Enums.Trigger.TriggerCondition.Schedule).ToList();
        }

        foreach (Trigger trigger in triggers)
        {
            _timer = new Timer(RefreshAsync, trigger, TimeSpan.Zero, TimeSpan.FromSeconds(trigger.ScheduleTime));
        }

        return Task.CompletedTask;
    }

    private async void RefreshAsync(object state)
    {
        Trigger trigger = state as Trigger;
    }
}
