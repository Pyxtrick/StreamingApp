using AutoMapper;
using Microsoft.Extensions.Configuration;
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

    public TwitchApiRequest(ITwitchCache twitchCache, IConfiguration configuration, ITwitchCallCache twitchCallCache, IMapper mapper)
    {
        _twitchCache = twitchCache;
        _configuration = configuration;
        _twitchCallCache = twitchCallCache;
        _mapper = mapper;
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

    public void Bot_OnMessageReceived2(object sender, OnMessageReceivedArgs e)
    {
        Bot_OnMessageReceived(sender, e);
    }

    public void Bot_OnMessageReceived(object sender, OnMessageReceivedArgs e)
    {
        string userId = e.ChatMessage.UserId;
        string userName = e.ChatMessage.DisplayName;
        string colorHex = e.ChatMessage.ColorHex;
        string message = e.ChatMessage.Message;
        string emoteReplacedMessage = e.ChatMessage.EmoteReplacedMessage;
        string replayMessage = "";
        var emoteSet = e.ChatMessage.EmoteSet;

        var sub = e.ChatMessage.IsSubscriber;

        string pointRediam = e.ChatMessage.CustomRewardId;
        int bits = e.ChatMessage.Bits;

        List<KeyValuePair<string, string>> badges = e.ChatMessage.Badges;

        List<AuthEnum> auths = new List<AuthEnum>()
        {
            e.ChatMessage.IsBroadcaster ? AuthEnum.Streamer : AuthEnum.undefined,
            e.ChatMessage.IsModerator ? AuthEnum.Mod : AuthEnum.undefined,
            e.ChatMessage.IsStaff ? AuthEnum.Staff : AuthEnum.undefined,
            e.ChatMessage.IsVip ? AuthEnum.Vip : AuthEnum.undefined,
            e.ChatMessage.IsSubscriber ? AuthEnum.Subscriber : AuthEnum.undefined,
            e.ChatMessage.IsTurbo ? AuthEnum.Turbo : AuthEnum.undefined,
            e.ChatMessage.IsPartner ? AuthEnum.Partner : AuthEnum.undefined,
        }.Where(a => a != 0).ToList();

        List<SpecialMessgeEnum> specialMessage = new List<SpecialMessgeEnum>()
        {
            e.ChatMessage.IsFirstMessage ? SpecialMessgeEnum.FirstMessage : 0,
            e.ChatMessage.IsHighlighted ? SpecialMessgeEnum.Highlighted : 0,
            e.ChatMessage.IsSkippingSubMode ? SpecialMessgeEnum.SkippSubMode : 0,
        }.Where(a => a != 0).ToList();

        //TODO: Check if this works on its own
        MessageDto messageDto1 = _mapper.Map<MessageDto>(e.ChatMessage);

        MessageDto messageDto = new(e.ChatMessage.Id, false, userId, userName, colorHex, replayMessage, message, emoteReplacedMessage, pointRediam, bits, emoteSet, badges, ChatOriginEnum.Twtich, auths, specialMessage, EffectEnum.none, e.ChatMessage.IsSubscriber, e.ChatMessage.SubscribedMonthCount, DateTime.UtcNow);

        //TODO: only save chat messages
        _twitchCallCache.AddMessage(messageDto1, CallCacheEnum.CachedMessageData);
    }

    public void Bot_OnChatCommandRecived(object sender, OnChatCommandReceivedArgs e)
    {
        string commandText = e.Command.CommandText.ToLower();
        string userName = e.Command.ChatMessage.DisplayName;

        List<AuthEnum> auths = new List<AuthEnum>()
        {
            e.Command.ChatMessage.IsBroadcaster ? AuthEnum.Streamer : AuthEnum.undefined,
            e.Command.ChatMessage.IsModerator ? AuthEnum.Mod : AuthEnum.undefined,
            e.Command.ChatMessage.IsStaff ? AuthEnum.Staff : AuthEnum.undefined,
            e.Command.ChatMessage.IsVip ? AuthEnum.Vip : AuthEnum.undefined,
            e.Command.ChatMessage.IsSubscriber ? AuthEnum.Subscriber : AuthEnum.undefined,
            e.Command.ChatMessage.IsTurbo ? AuthEnum.Turbo : AuthEnum.undefined,
            e.Command.ChatMessage.IsPartner ? AuthEnum.Partner : AuthEnum.undefined,
        }.Where(a => a != 0).ToList();

        //TODO: CommandDto commandDto = _mapper.Map<CommandDto>(e.ChatMessage);

        CommandDto commandDto = new(e.Command.ChatMessage.Id, e.Command.ChatMessage.UserId, e.Command.ChatMessage.DisplayName, e.Command.CommandText.ToLower(), auths, DateTime.UtcNow, ChatOriginEnum.Twtich);

        //TODO: Save commandDto to Cache
    }

    public async void Bot_OnGiftedSubscription(object sender, OnGiftedSubscriptionArgs e)
    {
        string? userName = e.GiftedSubscription.DisplayName;
        TwitchLib.Client.Enums.SubscriptionPlan subscriptionPlan = e.GiftedSubscription.MsgParamSubPlan;
        string amount = e.GiftedSubscription.MsgParamMonths;
        string lenght = e.GiftedSubscription.MsgParamMultiMonthGiftDuration;
        string resiveUserName = e.GiftedSubscription.MsgParamRecipientUserName;
        string emoteMessage;

        TierEnum tier = (TierEnum)Enum.Parse(typeof(TierEnum), subscriptionPlan.ToString());

        //TODO: SubscriptionDto mess = _mapper.Map<SubscriptionDto>(e.GiftedSubscription);
        SubscriptionDto subscriptionDto = new SubscriptionDto(e.GiftedSubscription.Id, userName, true, 1, tier, null);

        // TODO: Save Giffted Sub to Cache to be able to count amount of subs
        // TODO: Show emote
        // TODO: SaveSubscription to DB
        // TODO: SaveGiftedSubscription to DB
    }

    public async void Bot_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
    {
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

            // TODO: Show emote with Text
            // TODO: SaveSubscription to DB
        }
    }

    public async void Bot_OnPrimePaidSubscriber(object sender, OnPrimePaidSubscriberArgs e)
    {
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

            // TODO: Show emote with Text
            // TODO: SaveSubscription to DB
        }
    }

    public void Bot_OnReSubscriber(object sender, OnReSubscriberArgs e)
    {
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
            e.ReSubscriber.IsModerator ? AuthEnum.Mod : AuthEnum.undefined,
            e.ReSubscriber.IsSubscriber ? AuthEnum.Subscriber : AuthEnum.undefined,
            e.ReSubscriber.IsTurbo ? AuthEnum.Turbo : AuthEnum.undefined,
            e.ReSubscriber.IsPartner ? AuthEnum.Partner : AuthEnum.undefined,
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

        // TODO: SaveMessage to Cache
        // TODO: Show emote with Message
        // TODO: SaveSubscription to DB
    }

    public void Bot_OnRaidNotification(object sender, OnRaidNotificationArgs e)
    {
        string? userName = e.Channel;
        string amount = e.Channel;

        // TODO: Send a Shoutout to POST https://api.twitch.tv/helix/chat/shoutouts
        //https://dev.twitch.tv/docs/api/reference/#send-a-shoutout

        // TODO: Show emote

        //TODO: ChatDto chatDto = _mapper.Map<ChatDto>(e.ReSubscriber);

        throw new NotImplementedException();
    }

    public void Bot_OnUserBanned(object sender, OnUserBannedArgs e)
    {
        // TODO: Save to DB

        //TODO: BannedUserDto subscriptionDto = _mapper.Map<BannedUserDto>(e.ReSubscriber);
        BannedUserDto subscriptionDto = new(e.UserBan.Username, "test", DateTime.Now);
        throw new NotImplementedException();
    }

    public void Bot_OnUserJoined(object sender, OnUserJoinedArgs e)
    {
        // TODO: Save to DB
        Console.WriteLine(e);
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
