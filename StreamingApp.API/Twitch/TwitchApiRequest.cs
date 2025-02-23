using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using StreamingApp.API.SignalRHub;
using StreamingApp.API.Twitch.Interfaces;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Domain.Entities.Dtos;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Enums;
using TwitchLib.Api.Helix.Models.HypeTrain;
using TwitchLib.Client.Events;

namespace StreamingApp.API.Twitch;

public class TwitchApiRequest : ITwitchApiRequest
{
    private readonly ITwitchCache _twitchCache;
    private readonly IConfiguration _configuration;
    private readonly ITwitchCallCache _twitchCallCache;
    private readonly IMapper _mapper;
    private readonly IHubContext<ChatHub> _hubContext;

    public TwitchApiRequest(ITwitchCache twitchCache, IConfiguration configuration, ITwitchCallCache twitchCallCache, IMapper mapper, IHubContext<ChatHub> hubContext)
    {
        _twitchCache = twitchCache;
        _configuration = configuration;
        _twitchCallCache = twitchCallCache;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    public void Client_OnConnected(object sender, OnConnectedArgs e)
    {
        Log($"User {e.BotUsername} connected (bot access)");
    }

    public void OwnerOfChannel_OnDisconnected(object sender, TwitchLib.Communication.Events.OnDisconnectedEventArgs e)
    {
        Log($"OnDisconnectet event");
    }

    public void OwnerOfChannelConnection_OnLog(object sender, OnLogArgs e)
    {
        Log($"OnLog: {e.Data}");
    }

    public void Bot_OnChannelStateChanged(object sender, OnChannelStateChangedArgs e)
    {
        //throw new NotImplementedException();
    }

    public void Bot_OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        if (e.ChatMessage.Bits != 0)
        {
            OnHypeTrain();
        }

        MessageDto messageDto = _mapper.Map<MessageDto>(e.ChatMessage);
        messageDto.IsCommand = messageDto.Message.Split(' ').ToList()[0].Contains("!");

        _twitchCallCache.AddMessage(messageDto, CallCacheEnum.CachedMessageData);
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
        TwitchLib.Client.Enums.SubscriptionPlan subscriptionPlan = e.GiftedSubscription.MsgParamSubPlan;
        string amount = e.GiftedSubscription.MsgParamMonths;
        string lenght = e.GiftedSubscription.MsgParamMultiMonthGiftDuration;
        string resiveUserName = e.GiftedSubscription.MsgParamRecipientUserName;
        string emoteMessage;

        TierEnum tier = (TierEnum)Enum.Parse(typeof(TierEnum), subscriptionPlan.ToString());

        //TODO: SubscriptionDto mess = _mapper.Map<SubscriptionDto>(e.GiftedSubscription);
        SubscriptionDto subscriptionDto = new SubscriptionDto(e.GiftedSubscription.Id, userName, true, 1, tier, null);

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
            int cumulativeMonths = -1;
            string emoteMessage = "";

            //await UpdateSub(e.Subscriber, null);

            TierEnum tier = (TierEnum)Enum.Parse(typeof(TierEnum), subscriptionPlan);

            //TODO: SubscriptionDto subscriptionDto = _mapper.Map<SubscriptionDto>(e.Subscriber);
            SubscriptionDto subscriptionDto = new SubscriptionDto(e.Subscriber.Id, userName, true, 1, tier, null);

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

                Log($"parse MsgParamCumulativeMonths failed: {e.Subscriber.MsgParamCumulativeMonths}");
            }

            _twitchCallCache.AddMessage(subscriptionDto, CallCacheEnum.CachedSubData);

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

            int cumulativeMonths = -1;

            string emoteMessage = "";

            //await UpdateSub(null, e.PrimePaidSubscriber);

            TierEnum tier = (TierEnum)Enum.Parse(typeof(TierEnum), subscriptionPlan);

            //TODO: SubscriptionDto subscriptionDto = _mapper.Map<SubscriptionDto>(e.PrimePaidSubscriber);
            SubscriptionDto subscriptionDto = new SubscriptionDto(e.PrimePaidSubscriber.Id, userName, true, 1, tier, null);

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

                Log($"parse MsgParamCumulativeMonths failed: {e.PrimePaidSubscriber.MsgParamCumulativeMonths}");
            }

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
        var message = e.ReSubscriber.ResubMessage;
        int cumulativeMonths = -1;

        string emoteMessage = "";

        string colorHex = e.ReSubscriber.ColorHex;
        List<KeyValuePair<string, string>> badges = e.ReSubscriber.Badges;

        List<AuthEnum> auths = new List<AuthEnum>()
        {
            e.ReSubscriber.IsModerator ? AuthEnum.Mod : AuthEnum.Undefined,
            e.ReSubscriber.IsSubscriber ? AuthEnum.Subscriber : AuthEnum.Undefined,
            e.ReSubscriber.IsTurbo ? AuthEnum.Turbo : AuthEnum.Undefined,
            e.ReSubscriber.IsPartner ? AuthEnum.Partner : AuthEnum.Undefined,
        }.Where(a => a != 0).ToList();

        TierEnum tier = (TierEnum)Enum.Parse(typeof(TierEnum), subscriptionPlan.ToString());

        List<SpecialMessgeEnum> specialMessage = new List<SpecialMessgeEnum>()
        {
            SpecialMessgeEnum.SubMessage
        };

        //TODO: ChatDto chatDto = _mapper.Map<ChatDto>(e.ReSubscriber);
        //TODO: SubscriptionDto subscriptionDto = _mapper.Map<SubscriptionDto>(e.ReSubscriber);

        ChatDto chatDto = new(e.ReSubscriber.Id, userName, colorHex, "", message, "", null, badges, ChatOriginEnum.Twtich, null, auths, specialMessage, EffectEnum.none, DateTime.UtcNow);
        SubscriptionDto subscriptionDto = new SubscriptionDto(e.ReSubscriber.Id, userName, true, 1, tier, chatDto);

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

            Log($"parse MsgParamCumulativeMonths failed: {e.ReSubscriber.MsgParamCumulativeMonths}");
            Log($"parse MsgParamCumulativeMonths failed: {e.ReSubscriber.MsgParamStreakMonths}");
        }

        _twitchCallCache.AddMessage(subscriptionDto, CallCacheEnum.CachedSubData);

        // TODO: SaveMessage to Cache
        // TODO: Show emote with Message
        // TODO: SaveSubscription to DB
    }

    public void Bot_OnRaidNotification(object sender, OnRaidNotificationArgs e)
    {
        string? userName = e.RaidNotification.DisplayName;
        string amount = e.RaidNotification.SystemMsgParsed;

        // TODO: Send a Shoutout to POST https://api.twitch.tv/helix/chat/shoutouts
        //https://dev.twitch.tv/docs/api/reference/#send-a-shoutout

        // TODO: Show emote / alert

        //TODO: ChatDto chatDto = _mapper.Map<ChatDto>(e.ReSubscriber);

        // TODO: Set the user who raided in the User.Status.IsRaider to true
        //userName User.Status.IsRaider == true

        Console.WriteLine($"New Raid by {userName} with {amount} Users");

        //RaidDto raidDto = new()

        //_twitchCallCache.AddMessage(raidDto, CallCacheEnum.CachedRaidData);

        //throw new NotImplementedException();
    }

    public void Bot_OnUserBanned(object sender, OnUserBannedArgs e)
    {
        BannedUserDto bannedUser = new(e.UserBan.TargetUserId, e.UserBan.Username, "message", e.UserBan.BanReason, BannedTargetEnum.Banned, DateTime.Now);
        
        _twitchCallCache.AddMessage(bannedUser, CallCacheEnum.CachedBannedData);
    }

    public void Bot_OnUserTimedout(object sender, OnUserTimedoutArgs e)
    {
        BannedUserDto userTimeOout = new(e.UserTimeout.TargetUserId, e.UserTimeout.Username, "message", e.UserTimeout.TimeoutReason, BannedTargetEnum.TimeOut, DateTime.Now);
        
        _twitchCallCache.AddMessage(userTimeOout, CallCacheEnum.CachedBannedData);
    }

    public void Bot_OnMessageCleared(object sender, OnMessageClearedArgs e)
    {
        BannedUserDto deletedMessage = new(e.TargetMessageId, "UserName", e.Message, "Reson", BannedTargetEnum.Message, DateTime.Now);
        
        _twitchCallCache.AddMessage(deletedMessage, CallCacheEnum.CachedBannedData);
    }

    public void Bot_OnUserJoined(object sender, OnUserJoinedArgs e)
    {
        // TODO: Save to DB
        Console.WriteLine("Check this if it also does it when user followed");
        Console.WriteLine($"{e.Username} joined on {DateTime.Now} CET");
        //throw new NotImplementedException();
    }

    public async void OnHypeTrain()
    {
        GetHypeTrainResponse hypeTrain = await _twitchCache.GetTheTwitchAPI().Helix.HypeTrain.GetHypeTrainEventsAsync(_configuration["Twitch:ClientId"], 1, null);

        var eventData = hypeTrain.HypeTrain[0].EventData;

        if (DateTime.Parse(eventData.ExpiresAt) >= DateTime.Now)
        {
            int Level = eventData.Level;
            int total = eventData.Total;
            int goal = eventData.Goal;
            int persantage = 100 / goal * total;

            // TODO: Save Hypetrain To cache
        }
    }

    private void Log(string message)
    {
        //Invoke(new MethodInvoker(delegate { Log(message); }));
        Console.WriteLine(message);
    }
}
