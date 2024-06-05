using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace StreamingApp.API.Twitch.Interfaces;

public interface ITwitchApiRequest
{
    Task Bot_OnMessageReceived(object sender, OnMessageReceivedArgs e);
    void Bot_OnMessageReceived2(object sender, OnMessageReceivedArgs e);
    void Bot_OnChatCommandRecived(object sender, OnChatCommandReceivedArgs e);
    void Client_OnConnected(object sender, OnConnectedArgs e);
    void OwnerOfChannelConnection_OnLog(object sender, OnLogArgs e);
    void OwnerOfChannel_OnDisconnected(object sender, OnDisconnectedEventArgs e);
    void Bot_OnGiftedSubscription(object sender, OnGiftedSubscriptionArgs e);
    void Bot_OnNewSubscriber(object sender, OnNewSubscriberArgs e);
    void Bot_OnReSubscriber(object sender, OnReSubscriberArgs e);
    void Bot_OnPrimePaidSubscriber(object sender, OnPrimePaidSubscriberArgs e);
    void Bot_OnRaidNotification(object sender, OnRaidNotificationArgs e);
    void Bot_OnUserBanned(object sender, OnUserBannedArgs e);
    void Bot_OnUserJoined(object sender, OnUserJoinedArgs e);
}