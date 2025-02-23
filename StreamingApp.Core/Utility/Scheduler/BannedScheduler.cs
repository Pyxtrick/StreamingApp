using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Enums;

namespace StreamingApp.Core.Utility.Scheduler;

public class BannedScheduler : BackgroundService
{
    private readonly ILogger<BannedScheduler> _logger;

    private readonly IServiceProvider _serviceProvider;

    private readonly IConfiguration _configuration;

    private Timer _timer;

    private int timer = 0;

    public BannedScheduler(IServiceProvider serviceProvider, ILogger<BannedScheduler> logger, IConfiguration configuration)
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
            //TODO: Check if 1 second is enouth for this
            List<object> value = scope.ServiceProvider.GetRequiredService<ITwitchCallCache>().GetAllMessagesFromTo(DateTime.UtcNow.AddSeconds(timer * -1), DateTime.UtcNow, CallCacheEnum.CachedBannedData);

            if (value.Count != 0)
            {

                List<BannedUserDto> messages = value.ConvertAll(s => (BannedUserDto)s);

                Console.WriteLine(messages.Count);

                //await scope.ServiceProvider.GetRequiredService<IManageMessages>().ExecuteMultiple(messages);
            }
        }
    }
}
