using Microsoft.Extensions.Logging;
using StreamingApp.API.Twitch.Interfaces;
using TwitchLib.PubSub.Events;

namespace StreamingApp.API.Twitch;

public class TwitchPubSubApiRequest : ITwitchPubSubApiRequest
{
    private readonly ILogger<TwitchPubSubApiRequest> _logger;

    public TwitchPubSubApiRequest(ILogger<TwitchPubSubApiRequest> logger)
    {
        _logger = logger;
    }

    public void Bot_OnRewardRedeemed(object sedner, OnChannelPointsRewardRedeemedArgs e)
    {
        _logger.Log(LogLevel.Information, $"Pubsub event Reward {e.RewardRedeemed.Redemption.Reward.Title}");
    }

    public void Bot_OnLog(object sender, OnLogArgs e)
    {
        _logger.Log(LogLevel.Information, $"Pubsub event Log {e.Data}");
    }

    public void Bot_OnEmoteOnly(object sender, OnEmoteOnlyArgs e)
    {
        _logger.Log(LogLevel.Information, $"Pubsub event EmoteOnly {e.ChannelId}");
    }

    public void Bot_OnPubSubServiceConnected(object sender, EventArgs e)
    {
        _logger.Log(LogLevel.Information, $"Pubsub event Connected");
    }

    public void Bot_OnFollow(object sender, OnFollowArgs e)
    {
        _logger.Log(LogLevel.Information, $"Pubsub event OnFollow {e.DisplayName}");
    }
}
