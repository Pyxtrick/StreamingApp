using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace StreamingApp.API.Twitch.Interfaces;

public interface ITwitchApiRequest
{
    void Bot_OnMessageReceived(object sender, OnMessageReceivedArgs e);
    void Bot_OnChannelStateChanged(object sender, OnChannelStateChangedArgs e);
    void Bot_OnChatCommandRecived(object sender, OnChatCommandReceivedArgs e);
    void Client_OnConnected(object sender, OnConnectedArgs e);
    void OwnerOfChannelConnection_OnLog(object sender, OnLogArgs e);
    void OwnerOfChannel_OnDisconnected(object sender, OnDisconnectedEventArgs e);
    void Bot_OnRaidNotification(object sender, OnRaidNotificationArgs e);

    void Bot_OnGiftedSubscription(object sender, OnGiftedSubscriptionArgs e);
    void Bot_OnNewSubscriber(object sender, OnNewSubscriberArgs e);
    void Bot_OnReSubscriber(object sender, OnReSubscriberArgs e);
    void Bot_OnPrimePaidSubscriber(object sender, OnPrimePaidSubscriberArgs e);


    void Bot_OnUserBanned(object sender, OnUserBannedArgs e);
    void Bot_OnUserTimedout(object sender, OnUserTimedoutArgs e);
    void Bot_OnMessageCleared(object sender, OnMessageClearedArgs e);


    #region toTest
    void Bot_OnUserJoined(object sender, OnUserJoinedArgs e);
    void Bot_OnSendReceiveData(object sender, OnSendReceiveDataArgs e);
    void Bot_OnUnaccountedFor(object sender, OnUnaccountedForArgs e);
    #endregion
}