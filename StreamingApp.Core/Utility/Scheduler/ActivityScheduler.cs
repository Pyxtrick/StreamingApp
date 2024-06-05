using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StreamingApp.Core.Commands.Twitch.Interfaces;

namespace StreamingApp.Core.Utility.Scheduler;

public class ActivityScheduler : BackgroundService
{
    private readonly ILogger<ActivityScheduler> _logger;

    private readonly ActivitySchedulerOptions _options;

    private readonly IServiceProvider _serviceProvider;

    //private readonly ITwitchCallCache _twitchCallCache;

    private Timer _timer;

    public ActivityScheduler(IOptions<ActivitySchedulerOptions> options, IServiceProvider serviceProvider, ILogger<ActivityScheduler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        ArgumentNullException.ThrowIfNull(options);
        _options = options.Value;

        //_twitchCallCache = twitchCallCache;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return base.StopAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // change if needed to a better system
        var time = 1;
        _timer = new Timer(RefreshAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(time));

        return Task.CompletedTask;
    }

    private async void RefreshAsync(object state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            await scope.ServiceProvider.GetRequiredService<IManageMessages>().Execute();
        }
    }
}
