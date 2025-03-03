using TwitchLib.PubSub.Events;

namespace StreamingApp.API.Twitch.Interfaces;
public interface ITwitchPubSubApiRequest
{
    void Bot_OnEmoteOnly(object sender, OnEmoteOnlyArgs e);
    void Bot_OnFollow(object sender, OnFollowArgs e);
    void Bot_OnLog(object sender, OnLogArgs e);
    void Bot_OnPubSubServiceConnected(object sender, EventArgs e);
    void Bot_OnRewardRedeemed(object sedner, OnChannelPointsRewardRedeemedArgs e);
}