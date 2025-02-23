using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Utility.Scheduler;

public class AlertScheduler : BackgroundService
{
    private readonly ILogger<AlertScheduler> _logger;

    private readonly IServiceProvider _serviceProvider;

    private readonly IConfiguration _configuration;

    private Timer _timer;

    private int timer = 0;

    public AlertScheduler(IServiceProvider serviceProvider, ILogger<AlertScheduler> logger, IConfiguration configuration)
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
        _timer = new Timer(RefreshAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(timer));

        return Task.CompletedTask;
    }

    private async void RefreshAsync(object state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            //TODO: Combine all Three into one AlertDto
            List<object> fallow = scope.ServiceProvider.GetRequiredService<ITwitchCallCache>().GetAllMessagesFromTo(DateTime.UtcNow.AddSeconds(timer * -1), DateTime.UtcNow, CallCacheEnum.CachedUserFollowData);
            List<object> sub = scope.ServiceProvider.GetRequiredService<ITwitchCallCache>().GetAllMessagesFromTo(DateTime.UtcNow.AddSeconds(timer * -1), DateTime.UtcNow, CallCacheEnum.CachedSubData);
            List<object> raid = scope.ServiceProvider.GetRequiredService<ITwitchCallCache>().GetAllMessagesFromTo(DateTime.UtcNow.AddSeconds(timer * -1), DateTime.UtcNow, CallCacheEnum.CachedRaidData);

            /**if (value.Count != 0)
            {

                List<MessageDto> messages = value.ConvertAll(s => (MessageDto)s);

                Console.WriteLine(messages.Count);

                await scope.ServiceProvider.GetRequiredService<IManageMessages>().ExecuteMultiple(messages);
            }**/
        }
    }
}
