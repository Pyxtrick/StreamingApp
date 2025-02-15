using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StreamingApp.API.SignalRHub;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Utility.Scheduler;

public class ActivityScheduler : BackgroundService
{
    private readonly ILogger<ActivityScheduler> _logger;

    private readonly ActivitySchedulerOptions _options;

    private readonly IServiceProvider _serviceProvider;

    private Timer _timer;

    public ActivityScheduler(IOptions<ActivitySchedulerOptions> options, IServiceProvider serviceProvider, ILogger<ActivityScheduler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        ArgumentNullException.ThrowIfNull(options);
        _options = options.Value;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return base.StopAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // change if needed to a better system
        var time = 1; //_configuration["RefreshDelay:ChatRefresh"]
        _timer = new Timer(RefreshAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(time));

        return Task.CompletedTask;
    }

    private async void RefreshAsync(object state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            //TODO: Check if 1 second is enouth for this
            List<object> value = scope.ServiceProvider.GetRequiredService<ITwitchCallCache>().GetAllMessagesFromTo(DateTime.UtcNow.AddSeconds(-3), DateTime.UtcNow, CallCacheEnum.CachedMessageData);
            if (value.Count != 0)
            {
                List<MessageDto> messages = value.ConvertAll(s => (MessageDto)s);

                Console.WriteLine(messages.Count);

                await scope.ServiceProvider.GetRequiredService<IManageMessages>().ExecuteMultiple(messages);
            }
        }
    }
}
