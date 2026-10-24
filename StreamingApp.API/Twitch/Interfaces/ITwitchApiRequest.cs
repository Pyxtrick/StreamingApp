using TwitchLib.Client.Events;
using TwitchLib.Communication.Events;

namespace StreamingApp.API.Twitch.Interfaces;

public interface ITwitchApiRequest
{
    Task Bot_OnMessageReceived(object sender, OnMessageReceivedArgs e);
    Task Bot_OnChannelStateChanged(object sender, OnChannelStateChangedArgs e);
    Task Bot_OnChatCommandRecived(object sender, OnChatCommandReceivedArgs e);
    Task Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedEventArgs e);
    Task OwnerOfChannelConnection_OnLog(object sender, OnDisconnectedArgs e);
    Task OwnerOfChannel_OnDisconnected(object sender, OnDisconnectedArgs e);
    Task Bot_OnRaidNotification(object sender, OnRaidNotificationArgs e);
    
    Task Bot_OnGiftedSubscription(object sender, OnGiftedSubscriptionArgs e);
    Task Bot_OnNewSubscriber(object sender, OnNewSubscriberArgs e);
    Task Bot_OnReSubscriber(object sender, OnReSubscriberArgs e);
    Task Bot_OnPrimePaidSubscriber(object sender, OnPrimePaidSubscriberArgs e);
    

    Task Bot_OnUserBanned(object sender, OnUserBannedArgs e);
    Task Bot_OnUserTimedout(object sender, OnUserTimedoutArgs e);
    Task Bot_OnMessageCleared(object sender, OnMessageClearedArgs e);


    #region toTest
    Task Bot_OnUserJoined(object sender, OnUserJoinedArgs e);
    Task Bot_OnSendReceiveData(object sender, OnSendReceiveDataArgs e);
    Task Bot_OnUnaccountedFor(object sender, OnUnaccountedForArgs e);
    #endregion
}