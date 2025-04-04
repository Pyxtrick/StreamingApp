﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Utility.Scheduler;

public class YoutubeScheduler : BackgroundService
{
    private readonly ILogger<MessageScheduler> _logger;

    private readonly IServiceProvider _serviceProvider;

    private readonly IConfiguration _configuration;

    private Timer _timer;

    private int timer = 0;

    public YoutubeScheduler(IServiceProvider serviceProvider, ILogger<MessageScheduler> logger, IConfiguration configuration)
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
            /**
            List<object> youtubeMessages = scope.ServiceProvider.GetRequiredService<ITwitchCallCache>().GetAllMessagesFromTo(DateTime.UtcNow.AddSeconds(timer * -1), DateTime.UtcNow, CallCacheEnum.CachedYoutubeMessageData);

            if (youtubeMessages.Count != 0)
            {
                try
                {
                    youtubeMessages.ConvertAll(s => (MessageDto)s).Select(async message => await scope.ServiceProvider.GetRequiredService<IManageMessages>().ExecuteOne(message));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }**/
        }
    }
}
