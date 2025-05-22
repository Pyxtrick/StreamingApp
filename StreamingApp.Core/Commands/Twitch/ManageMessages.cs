using Microsoft.EntityFrameworkCore;
using StreamingApp.API.Utility.Caching.Interface;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.Core.Commands.Hub;
using StreamingApp.Core.Commands.Twitch.Interfaces;
using StreamingApp.Core.Queries.Logic.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos.Twitch;
using StreamingApp.Domain.Entities.InternalDB.User;
using StreamingApp.Domain.Enums;
using StreamingApp.Domain.Static;

namespace StreamingApp.Core.Commands.Twitch;
public class ManageMessages : IManageMessages
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly ICRUDUsers _crudUsers;

    private readonly ITwitchCallCache _twitchCallCache;

    private readonly IEmotesCache _emotesCache;

    private readonly ISendSignalRMessage _sendSignalRMessage;

    private readonly IMessageCheck _messageCheck;

    public ManageMessages(UnitOfWorkContext unitOfWork, ICRUDUsers crudUsers, ITwitchCallCache twitchCallCache, IEmotesCache emotesCache, ISendSignalRMessage sendSignalRMessage, IMessageCheck messageCheck)
    {
        _unitOfWork = unitOfWork;
        _crudUsers = crudUsers;
        _twitchCallCache = twitchCallCache;
        _emotesCache = emotesCache;
        _sendSignalRMessage = sendSignalRMessage;
        _messageCheck = messageCheck;
    }

    /// <summary>
    /// Get All Chached Twitch Messages from the past second
    /// </summary>
    /// <returns></returns>
    public async Task Execute()
    {
        // TODO: get Seconds from appsettings | _configuration["RefreshDelay:ChatRefresh"]
        List<object> value = _twitchCallCache.GetAllUnusedMessages(CallCacheEnum.CachedMessageData);

        if (value.Count != 0)
        {
            List<MessageDto> messages = value.ConvertAll(s => (MessageDto)s);
            foreach (MessageDto message in messages)
            {
                await ExecuteOne(message);
            }
        }
    }

    public async Task ExecuteMultiple(List<MessageDto> messages)
    {
        foreach (var message in messages)
        {
            await ExecuteOne(message);
        }
    }

    public async Task ExecuteOne(MessageDto messageDto)
    {
        User user = _unitOfWork.User.Include("Ban").Include("Status").Include("Details").FirstOrDefault(u => u.Details.FirstOrDefault(t => t.Origin.ToString().Equals(messageDto.ChatOrigin.ToString())).ExternalUserId == messageDto.UserId);

        if (user != null)
        {
            // TODO: make backend check if this is the first message during the stream
            //messageDto.SpecialMessage.Add(SpecialMessgeEnum.FirstStreamMessage);

            await _crudUsers.UpdateAchievements(messageDto.UserId, messageDto.ChatOrigin);
        }
        else
        {
            user = await _crudUsers.CreateOne(messageDto.UserId, messageDto.UserName, messageDto.IsSub, messageDto.SubCount, messageDto.Auth, messageDto.ChatOrigin);
        }

        messageDto.Emotes.AddRange(MappEmotes(messageDto));
        messageDto.Badges = MappBadges(messageDto.Badges);

        bool combinedChat = false; // TODO: get data from somewere
        // Combined Chat
        // TODO: userId Not working
        if (combinedChat == true)
        {
            messageDto.Badges.Add(new(messageDto.Channel, messageDto.Channel.Contains("Pyxtrick", StringComparison.CurrentCultureIgnoreCase)
                ? "https://static-cdn.jtvnw.net/jtv_user_pictures/f0eb150a-0f70-4876-977a-7eabb557fa79-profile_image-70x70.png"
                : $"https://static-cdn.jtvnw.net/jtv_user_pictures/{""}-profile_image-70x70.png"));
        }

        // Check Message befor it is sent to the Frontend or anywhere else
        if (await _messageCheck.Execute(messageDto, user) == false)
        {
            //SpecialWords? allowedMessage = _unitOfWork.SpecialWords.FirstOrDefault(t => t.Name.Contains(messageDto.Message) && t.Type == SpecialWordEnum.AllowedUrl);
            return;
        }

        await _sendSignalRMessage.SendChatMessage(user, messageDto);
    }

    private List<KeyValuePair<string, string>> MappBadges(List<KeyValuePair<string, string>> userBadges)
    {
        List<KeyValuePair<string, string>> badges = new();

        foreach (var userBadge in userBadges)
        {
            // TODO: Get Badges form DB
            var allBadges = BadgesData.GetAllBadges();

            var badge = allBadges.FirstOrDefault(b => b.Value == userBadge.Key);

            if (badge.Value != null)
            {
                badges.Add(new(badge.Value, $"https://static-cdn.jtvnw.net/badges/v1/{badge.Key}/1"));
            }
        }

        return badges;
    }

    private List<EmoteSet>? MappEmotes(MessageDto messageDto)
    {
        var emotes = _emotesCache.GetEmotes(null);

        var foundEmotes = emotes.Where(e => messageDto.Message.Contains(e.Name)).ToList();

        List<EmoteSet> emoteList = new();

        if (foundEmotes.Any())
        {
            foreach (var emote in foundEmotes)
            {
                emoteList.Add(new EmoteSet() { Name = emote.Name, AnimatedURL = emote.Url, StaticURL = emote.Url });
            }
        }

        return emoteList;
    }
}
