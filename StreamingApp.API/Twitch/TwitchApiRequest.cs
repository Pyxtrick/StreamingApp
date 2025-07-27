using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;
using TwitchLib.Api.Helix.Models.HypeTrain;
using TwitchLib.Client.Events;
using WebSocketSharp;

namespace StreamingApp.API.Twitch;

public class TwitchApiRequest : ITwitchApiRequest
{
    private readonly ITwitchCache _twitchCache;
    private readonly IConfiguration _configuration;
    private readonly ITwitchCallCache _twitchCallCache;
    private readonly IMapper _mapper;

    private string RaidUser = "";

    public TwitchApiRequest(ITwitchCache twitchCache, IConfiguration configuration, ITwitchCallCache twitchCallCache, IMapper mapper)
    {
        _twitchCache = twitchCache;
        _configuration = configuration;
        _twitchCallCache = twitchCallCache;
        _mapper = mapper;
    }

    public void Client_OnConnected(object sender, OnConnectedArgs e)
    {
        Console.WriteLine($"User {e.BotUsername} connected (bot access)");
    }

    public void OwnerOfChannel_OnDisconnected(object sender, TwitchLib.Communication.Events.OnDisconnectedEventArgs e)
    {
        Console.WriteLine($"OnDisconnectet event");
    }

    public void OwnerOfChannelConnection_OnLog(object sender, OnLogArgs e)
    {
        Console.WriteLine($"OnLog: {e.Data}");
    }

    public void Bot_OnChannelStateChanged(object sender, OnChannelStateChangedArgs e)
    {
        //TODO: Is this when i go online and offline ?
        Console.WriteLine($"Chanel state Change: {e.ChannelState}");
        Console.WriteLine($"R9K: {e.ChannelState.R9K}"); // R9K == avoid users to send same message
        //throw new NotImplementedException();
    }

    public void Bot_OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        if (e.ChatMessage.Bits != 0)
        {
            OnHypeTrain();
        }
        
        // TODO: Test if this is valid
        if (e.ChatMessage.Username.Equals(RaidUser))
        {
            Console.WriteLine($"Raid User {RaidUser} chatted");
        }

        if(e.ChatMessage.Message.Contains("Followed ALOO"))
        {
            Console.WriteLine("Follow Message Resived");
        }

        if (e.ChatMessage.Bits != 0 || e.ChatMessage.CustomRewardId.IsNullOrEmpty() == false)
        {
            MessageAlertDto messageDto = _mapper.Map<MessageAlertDto>(e.ChatMessage);

            messageDto.AlertType = e.ChatMessage.Bits != 0 ? AlertTypeEnum.Bits : AlertTypeEnum.PointRedeam;

            _twitchCallCache.AddMessage(messageDto, CallCacheEnum.CachedAlertData);
        }
        else
        {
            MessageDto messageDto = _mapper.Map<MessageDto>(e.ChatMessage);
            messageDto.IsCommand = messageDto.Message.Split(' ').ToList()[0].Contains("!");

            if (!messageDto.Channel.Equals(_configuration["Twitch:Channel"]))
            {
                // TODO: Check in chared chat what is the difference
                Console.WriteLine($"message comes from channel {e.ChatMessage.Channel}, {JsonConvert.SerializeObject(messageDto.Badges)}");
            }

            _twitchCallCache.AddMessage(messageDto, CallCacheEnum.CachedMessageData);
        }
    }

    public void Bot_OnChatCommandRecived(object sender, OnChatCommandReceivedArgs e)
    {
        MessageDto commandDto = _mapper.Map<MessageDto>(e.Command.ChatMessage);
        commandDto.IsCommand = true;

        Console.WriteLine("Command Called");
        //_twitchCallCache.AddMessage(commandDto, CallCacheEnum.CachedMessageData);
    }

    public async void Bot_OnGiftedSubscription(object sender, OnGiftedSubscriptionArgs e)
    {
        OnHypeTrain();

        string? userName = e.GiftedSubscription.DisplayName;
        string recipientUserName = e.GiftedSubscription.MsgParamRecipientUserName;
        TwitchLib.Client.Enums.SubscriptionPlan subscriptionPlan = e.GiftedSubscription.MsgParamSubPlan;
        string amount = e.GiftedSubscription.MsgParamMonths;
        string lenght = e.GiftedSubscription.MsgParamMultiMonthGiftDuration;
        string resiveUserName = e.GiftedSubscription.MsgParamRecipientUserName;
        string emoteMessage;

        TierEnum tier = (TierEnum)Enum.Parse(typeof(TierEnum), subscriptionPlan.ToString());

        //TODO: SubscriptionDto mess = _mapper.Map<SubscriptionDto>(e.GiftedSubscription);
        SubscriptionDto subscriptionDto = new SubscriptionDto(e.GiftedSubscription.Id, userName, recipientUserName, true, 1, tier, null);

        _twitchCallCache.AddMessage(subscriptionDto, CallCacheEnum.CachedSubData);

        // TODO: Save Giffted Sub to Cache to be able to count amount of subs
        // TODO: Show emote
        // TODO: SaveSubscription to DB
        // TODO: SaveGiftedSubscription to DB
    }

    public async void Bot_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
    {
        OnHypeTrain();

        if (!e.Subscriber.SubscriptionPlan.Equals("Prime"))
        {
            string userName = e.Subscriber.DisplayName;
            string subscriptionPlan = e.Subscriber.SubscriptionPlan.ToString();
            int cumulativeMonths = int.Parse(e.Subscriber.MsgParamCumulativeMonths);
            string emoteMessage = "";

            SubDto subscriptionDto = _mapper.Map<SubDto>(e.Subscriber);
            //SubDto subDto = new SubDto(null, e.Subscriber.Id, userName, e.Subscriber.DisplayName, "channel", OriginEnum.Twitch, false, 0, 0, tier, null, false, DateTime.Now);

            try
            {
                cumulativeMonths = int.Parse(e.Subscriber.MsgParamCumulativeMonths);

                if (cumulativeMonths == 0)
                {   
                    emoteMessage = $"Thank you {userName} for joining us for the first month as an {subscriptionPlan} Sub";
                }
                else
                {
                    emoteMessage = $"Thank you {userName} for joining us for {cumulativeMonths} months as an {subscriptionPlan} Sub";
                }
            }
            catch (Exception)
            {
                emoteMessage = $"Thank you {userName} for joining us as an {subscriptionPlan} Sub";

                Console.WriteLine($"parse MsgParamCumulativeMonths failed: {e.Subscriber.MsgParamCumulativeMonths}");
            }

            _twitchCallCache.AddMessage(subscriptionDto, CallCacheEnum.CachedSubData);

            Console.WriteLine(emoteMessage);

            // TODO: Show emote with Text
            // TODO: SaveSubscription to DB
        }
    }

    public async void Bot_OnPrimePaidSubscriber(object sender, OnPrimePaidSubscriberArgs e)
    {
        OnHypeTrain();

        if (e.PrimePaidSubscriber.SubscriptionPlan.Equals("Prime"))
        {
            string userName = e.PrimePaidSubscriber.DisplayName;
            string subscriptionPlan = e.PrimePaidSubscriber.SubscriptionPlan.ToString();
            int cumulativeMonths = int.Parse(e.PrimePaidSubscriber.MsgParamCumulativeMonths);
            string emoteMessage = "";

            SubDto subscriptionDto = _mapper.Map<SubDto>(e.PrimePaidSubscriber);
            //SubDto subDto = new SubDto(null, e.PrimePaidSubscriber.Id, userName, e.PrimePaidSubscriber.DisplayName, e.Channel, OriginEnum.Twitch, false, 0, cumulativeMonths, tier, null, false, DateTime.Now);

            try
            {
                cumulativeMonths = int.Parse(e.PrimePaidSubscriber.MsgParamCumulativeMonths);

                if (cumulativeMonths == 1)
                {
                    emoteMessage = $"Thank you {userName} for joining us for the first month as an {subscriptionPlan} member";
                }
                else
                {
                    emoteMessage = $"Thank you {userName} for joining us for {cumulativeMonths} months as an {subscriptionPlan} member";
                }
            }
            catch (Exception)
            {
                emoteMessage = $"Thank you {userName} for joining us as an {subscriptionPlan} member";

                Console.WriteLine($"parse MsgParamCumulativeMonths failed: {e.PrimePaidSubscriber.MsgParamCumulativeMonths}");
            }

            Console.WriteLine(emoteMessage);

            _twitchCallCache.AddMessage(subscriptionDto, CallCacheEnum.CachedSubData);

            // TODO: Show emote with Text
            // TODO: SaveSubscription to DB
        }
    }

    public void Bot_OnReSubscriber(object sender, OnReSubscriberArgs e)
    {
        OnHypeTrain();

        string userName = e.ReSubscriber.DisplayName;
        int months = e.ReSubscriber.Months;
        string subscriptionPlan = e.ReSubscriber.SubscriptionPlan.ToString();
        int cumulativeMonths = int.Parse(e.ReSubscriber.MsgParamCumulativeMonths);
        string emoteMessage = "";

        SubDto subscriptionDto = _mapper.Map<SubDto>(e.ReSubscriber);
        //SubDto subDto = new SubDto(null, e.ReSubscriber.Id, userName, e.ReSubscriber.DisplayName, e.ReSubscriber.Channel, OriginEnum.Twitch, false, 0, cumulativeMonths, tier, null, false, DateTime.Now);

        try
        {
            cumulativeMonths = int.Parse(e.ReSubscriber.MsgParamCumulativeMonths);
            var streakMonths = e.ReSubscriber.MsgParamShouldShareStreak ? int.Parse(e.ReSubscriber.MsgParamStreakMonths) : 0;
            if (streakMonths > 0)
            {
                emoteMessage = $"Thank you {userName} for joining us for {cumulativeMonths} months as an {subscriptionPlan} member with a Streak of {streakMonths} months";
            }
            else
            {
                emoteMessage = $"Thank you {userName} for joining us for {cumulativeMonths} months as an {subscriptionPlan} member";
            }
        }
        catch (Exception)
        {
            emoteMessage = $"Thank you {userName} for {months} months of {subscriptionPlan} Sub";

            Console.WriteLine($"parse MsgParamCumulativeMonths failed: {e.ReSubscriber.MsgParamCumulativeMonths}");
            Console.WriteLine($"parse MsgParamCumulativeMonths failed: {e.ReSubscriber.MsgParamStreakMonths}");
        }

        Console.WriteLine(emoteMessage);

        _twitchCallCache.AddMessage(subscriptionDto, CallCacheEnum.CachedSubData);

        // TODO: Show emote with Message
        // TODO: SaveSubscription to DB
    }

    public void Bot_OnRaidNotification(object sender, OnRaidNotificationArgs e)
    {
        string? userName = e.RaidNotification.DisplayName;
        int amount = 0;

        // TODO: Test if this is valid
        RaidUser = e.RaidNotification.DisplayName;

        // TODO: Send a Shoutout to POST https://api.twitch.tv/helix/chat/shoutouts
        //https://dev.twitch.tv/docs/api/reference/#send-a-shoutout

        // TODO: Show emote / alert

        //TODO: ChatDto chatDto = _mapper.Map<ChatDto>(e.ReSubscriber);

        // TODO: Set the user who raided in the User.Status.IsRaider to true
        //userName User.Status.IsRaider == true

        Console.WriteLine($"New Raid by {userName} with {amount} Users");

        RaidDto raidDto = new(userName, amount, "", false, DateTime.UtcNow);

        //throw new NotImplementedException();
    }

    public void Bot_OnUserBanned(object sender, OnUserBannedArgs e)
    {
        BannedUserDto bannedUser = new(e.UserBan.TargetUserId, "", e.UserBan.Username, "message", e.UserBan.BanReason, BannedTargetEnum.Banned, false, OriginEnum.Twitch, DateTime.Now);
        
        _twitchCallCache.AddMessage(bannedUser, CallCacheEnum.CachedBannedData);
    }

    public void Bot_OnUserTimedout(object sender, OnUserTimedoutArgs e)
    {
        BannedUserDto userTimeOout = new(e.UserTimeout.TargetUserId, "", e.UserTimeout.Username, "message", e.UserTimeout.TimeoutReason, BannedTargetEnum.TimeOut, false, OriginEnum.Twitch, DateTime.Now);
        
        _twitchCallCache.AddMessage(userTimeOout, CallCacheEnum.CachedBannedData);
    }

    public void Bot_OnMessageCleared(object sender, OnMessageClearedArgs e)
    {
        BannedUserDto deletedMessage = new("", e.TargetMessageId, "UserName", e.Message, "Reson", BannedTargetEnum.Message, false, OriginEnum.Twitch, DateTime.Now);
        
        _twitchCallCache.AddMessage(deletedMessage, CallCacheEnum.CachedBannedData);
    }

    public async void OnHypeTrain()
    {
        GetHypeTrainResponse hypeTrain = await _twitchCache.GetTheTwitchAPI().Helix.HypeTrain.GetHypeTrainEventsAsync(_configuration["Twitch:ClientId"], 1, null);

        var eventData = hypeTrain.HypeTrain[0].EventData;

        Console.WriteLine($"OnHypeTrain ExpiresAt: {eventData.ExpiresAt} Current Time{DateTime.Now}");

        if (DateTime.Parse(eventData.ExpiresAt) >= DateTime.Now)
        {
            Console.WriteLine($"OnHypeTrain Active");

            int Level = eventData.Level;
            int total = eventData.Total;
            int goal = eventData.Goal;
            int persantage = 100 / goal * total;

            // TODO: Save Hypetrain To cache
        }
    }


    #region toTest
    public void Bot_OnUserJoined(object sender, OnUserJoinedArgs e)
    {
        // TODO: Save to DB
        //Console.WriteLine("Check this if it also does it when user followed");
        //Console.WriteLine($"{e.Username} joined on {DateTime.Now} CET");
        //throw new NotImplementedException();
    }

    public void Bot_OnSendReceiveData(object sender, OnSendReceiveDataArgs e)
    {
        Console.WriteLine($"OnSendReceiveDataArgs Data: {e.Data}");
    }
    public void Bot_OnUnaccountedFor(object sender, OnUnaccountedForArgs e)
    {
        Console.WriteLine($"OnUnaccountedForArgs {e.RawIRC}");
    }
    #endregion
}
