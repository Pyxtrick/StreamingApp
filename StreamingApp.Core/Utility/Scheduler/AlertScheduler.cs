using AutoMapper.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.DB;
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
            var settings = scope.ServiceProvider.GetRequiredService<UnitOfWorkContext>().Settings.FirstOrDefault();

            if (settings.MuteAllerts == false)
            {
                List<object> alertObject = scope.ServiceProvider.GetRequiredService<ITwitchCallCache>().GetAllUnusedMessages(CallCacheEnum.CachedAlertData);
                List<object> subObject = scope.ServiceProvider.GetRequiredService<ITwitchCallCache>().GetAllUnusedMessages(CallCacheEnum.CachedSubData);
                List<object> raidObject = scope.ServiceProvider.GetRequiredService<ITwitchCallCache>().GetAllUnusedMessages(CallCacheEnum.CachedRaidData);

                if (alertObject.Any() == false)
                {
                    try
                    {
                        var alerts = alertObject.ConvertAll(s => (MessageAlertDto)s);

                        foreach (var alert in alerts)
                        {
                            await scope.ServiceProvider.GetRequiredService<IManageAchievements>().ExecuteBit(alert);
                            await scope.ServiceProvider.GetRequiredService<IManageAlert>().ExecuteBitAndRedeamAndFollow(alert);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }

                if (subObject.Any() == false)
                {
                    try
                    {
                        var subs = subObject.ConvertAll(s => (SubDto)s);

                        foreach (var sub in subs)
                        {
                            await scope.ServiceProvider.GetRequiredService<IManageAchievements>().ExecuteSub(sub);
                            await scope.ServiceProvider.GetRequiredService<IManageAlert>().ExecuteSub(sub);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }

                if (raidObject.Any() == false)
                {
                    try
                    {
                        var raids = raidObject.ConvertAll(s => (RaidDto)s);

                        foreach (var raid in raids)
                        {
                            await scope.ServiceProvider.GetRequiredService<IManageAlert>().ExecuteRaid(raid);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
        }
    }
}
